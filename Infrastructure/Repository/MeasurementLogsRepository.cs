using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Repository
{
    public class MeasurementLogsRepository : IMeasurementLogsRepository
    {
        private readonly IMeasurementLogsManager<MeasurementLogs> _measLogsManager;
        private readonly IUserManager<User> _userManager;
        private readonly IMapper _mapper;


        public MeasurementLogsRepository(IMeasurementLogsManager<MeasurementLogs> measLogsManager, IUserManager<User> userManager, IMapper mapper)
        {
            _measLogsManager = measLogsManager;
            _userManager = userManager;
            _mapper = mapper;

        }

        public async Task<MeasurementLogsResponse> AddAsync(MeasurementLogsCreateRequest request)
        {
            var sharedFor = new List<User>();
            var valuesLogs = new List<ValuesLogs>();
            var teacher = await _userManager.GetByIdAsync(request.TeacherID);

            if (teacher == null) throw new Exception("Don't find user");
            if (request.SharedFor.Count > 0) sharedFor = await findUsersAsync(request.SharedFor);
            if (request.Values.Count > 0) parseToValueLogs(request.Values, ref valuesLogs);

            var model = new MeasurementLogs {
                Name = request.Name,
                Teacher = teacher,
                Values = valuesLogs,
                SharedFor = sharedFor
            };

            var addResult = await _measLogsManager.CreateAsync(model);
            var response = _mapper.Map<MeasurementLogsResponse>(addResult);
            return await Task.FromResult(response);

        }

        public async Task<MeasurementLogsResponse> Edit(MeasurementLogsUpdateRequest request)
        {
            if (!await _measLogsManager.CheckPersmission(request.ID)) throw new Exception("User doesn't have permission");

            var log = await _measLogsManager.GetById(request.ID);
            var parseToListId = log.SharedFor?.Select(x => x.ID).ToList();
            if (log == null) throw new Exception("Measurement Logs doesn't exist");
            if (!compareIntList(parseToListId, request.SharedFor)) log.SharedFor = await findUsersAsync(request.SharedFor);
            if (log.Teacher.ID != request.TeacherID) log.Teacher = await _userManager.GetByIdAsync(request.TeacherID);

            log.Name = request.Name;
            log.Values = updateValueLogs(log.Values, request.Values);

            await _measLogsManager.EditAsync(log);
            var result = _mapper.Map<MeasurementLogsResponse>(log);

            return await Task.FromResult(result);
        }

        public void Remove(MeasurementLogsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MeasurementLogsResponse>> Get()
        {
            var response =  _measLogsManager.Get();
            var map = _mapper.Map<IEnumerable<MeasurementLogsResponse>>(response);
            return await Task.FromResult(map);
        }

        public async Task<MeasurementLogsResponse> GetByIdAsync(int id)
        { 
            var response = await _measLogsManager.GetById(id);
            return _mapper.Map<MeasurementLogsResponse>(response);
        }

        public async Task<List<ValueLogsResponse>> GetValueByNameAsync(MeasurementLogsGetRequest request)
        {
            var response = new List<ValueLogsResponse>();
            var log = await _measLogsManager.GetById(request.ID);

            if (log is null) throw new Exception("Measurement Log doesn't exist");
            if (log.Values is null || log.Values.Count == 0) return response;
            if (request.ValueName is null) return _mapper.Map <List<ValueLogsResponse>>(log.Values);

            getValueByName(request.ValueName.ToList(), log.Values, ref response);
            return response;
        }

        public async Task<ValueLogsResponse> GetValueById(int id, int valueId)
        {
            var permission = await _measLogsManager.CheckPersmission(id);
            if(!permission) throw new Exception("User doesn't have permission");

            var log = await _measLogsManager.GetById(id);
            var response = log.Values.SingleOrDefault(x => x.ID == valueId);
            var mapper = _mapper.Map<ValueLogsResponse>(response);
            return mapper;
        }


        #region additional functions

        private void getValueByName(List<string> valueNames, List<ValuesLogs> destinationValueLogs, ref List<ValueLogsResponse> response)
        {
            foreach(var item in valueNames)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    var value = destinationValueLogs.SingleOrDefault(x => x.Name.ToLower() == item.ToLower());
                    if(value is not null)
                    {
                        var model = _mapper.Map<ValueLogsResponse>(value);
                        response.Add(model);
                    }
                }
            }
        }

        private async Task<List<User>> findUsersAsync(List<int> idList)
        {
            var users = new List<User>();
            foreach (var item in idList)
            {
                var user = await _userManager.GetByIdAsync(item);
                if (user != null) users.Add(user);
            }
            return users;
        }

        private void parseToValueLogs(List<ValuesLogsCreateRequest> request, ref List<ValuesLogs> valuesLogs)
        {
            foreach (var item in request)
            {
                var valueLog = new ValuesLogs
                {
                    Name = item.Name,
                    Value = item.Value ?? null
                };

                valuesLogs.Add(valueLog);
            }
        }

        private List<ValuesLogs> updateValueLogs(List<ValuesLogs> source, List<ValuesLogsUpdateRequest> update)
        {
            var values = new List<ValuesLogs>();
            foreach (var item in update)
            {
                if (item?.ID is null || item?.ID == 0)
                {
                    values.Add(new ValuesLogs
                    {
                        Name = item.Name,
                        Value = item.Value
                    });
                }
                else
                {
                    var value = source?.SingleOrDefault(x => x.ID == item.ID);
                    if (value != null)
                    {
                        value.Name = item.Name;
                        value.Value = item.Value;
                        value.Modified = DateTime.Now;

                        values.Add(value);
                    }
                }
            }
            return values;
        }

        private bool compareIntList(List<int> source, List<int> source2)
        {
            if (source is null || source2 is null) return false;
            if (source.Count != source2.Count) return false;
            foreach(var item in source)
            {
                if (!source2.Contains(item)) return false;
            }
            return true;
        }

        #endregion

    }
}

