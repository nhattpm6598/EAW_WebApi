using EAW_WebApi.Data.UnitOfWork;
using EAW_WebApi.Models;
using EAW_WebApi.ReuqestWinform;
using EAW_WebApi.ServiceWinform.Interface;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAW_WebApi.ServiceWinform
{
    public class RedisConnectionService :   IRedisConnectionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDistributedCache _redisCache;

        public RedisConnectionService(UnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _redisCache = cache;
        }

        public string updateRedis(AttendanceWinformRequest request)
        {
             return updateRedisAttendance(request);
        }

        #region update redis
        private string updateRedisAttendance(AttendanceWinformRequest request)
        {
            

            string machineCode = request.FaceMachineCode;
            string base64 = request.base64StringImg;
            DateTime time = request.createTime;

            string json = getRedisLastAttendance(machineCode);

            string _jsonRedis = _redisCache.GetString("AttendanceList");
            if (_jsonRedis == null)
            {
                List<AttendanceRedis> attendances = new List<AttendanceRedis>();
                attendances.Add(new AttendanceRedis()
                {
                    machineCode = machineCode,
                    base64 = base64,
                    createTime = time
                } );
                _jsonRedis = JsonSerializer.Serialize<List<AttendanceRedis>>(attendances);
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(2));
                _redisCache.SetString("AttendanceList", _jsonRedis, options);

            }
            else
            {
                JsonSerializerOptions opt = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                List<AttendanceRedis> data = JsonSerializer.Deserialize<List<AttendanceRedis>>(_jsonRedis, opt);
                var cacheAttendance = data.FirstOrDefault(p => p.machineCode == machineCode);
                if (cacheAttendance == null)
                {
                    data.Add(new AttendanceRedis()
                    {
                        machineCode = machineCode,
                        base64 = base64,
                        createTime = time
                    });

                }
                else
                {
                    cacheAttendance.base64 = base64;
                    cacheAttendance.createTime = time;
                }
                var jsonPlaylists = JsonSerializer.Serialize<List<AttendanceRedis>>(data);
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(2));
                _redisCache.SetString("AttendanceList", jsonPlaylists, options);

            }
            return json;
        }
        //get redis lastAttendance
        private string getRedisLastAttendance(string machineCode)
        {
            string json = null;
            string _jsonRedis = _redisCache.GetString("LastAttendence");
            if(_jsonRedis != null)
            {
                JsonSerializerOptions opt = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                List<LastAttendanceRedis> data = JsonSerializer.Deserialize<List<LastAttendanceRedis>>(_jsonRedis, opt);
                var cacheLastAttendance = data.FirstOrDefault(p => p.FaceMachineCode == machineCode);
                if (cacheLastAttendance != null)
                {
                    //***
                    String name = _unitOfWork.Repository<Employee>().Find(x => x.EmployeeCode == cacheLastAttendance.EmpCode).Name;
                    String AttendanceTime = cacheLastAttendance.createTime.ToString("dd MMMM yyyy hh:mm:ss tt");

                    return name + " - " + AttendanceTime;
                    
                }
            }
            return json;
        }
        #endregion

    }

    public class AttendanceRedis
    {
        public string machineCode { get; set; }
        public string base64 { get; set; }
        public DateTime createTime { get; set; }
    }

    public class LastAttendanceRedis
    {
        public string EmpCode { get; set; }
        public string FaceMachineCode { get; set; }
        public string Mode { get; set; }
        public string base64StringImg { get; set; }
        public DateTime createTime { get; set; }
    }
}
