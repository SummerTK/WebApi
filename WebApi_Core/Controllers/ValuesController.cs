using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Microsoft.Extensions.Logging;

namespace WebApi_Core.Controllers
{
    /// <summary>
    /// 测试页
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly ILogger<ValuesController> _logger;
        static Logger Logger = LogManager.GetCurrentClassLogger();
        public ValuesController(ILogger<ValuesController> logger)
        {
            this._logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Logger.Info("普通信息日志-----------");
            Logger.Debug("调试日志-----------");
            Logger.Error("错误日志-----------");
            Logger.Fatal("异常日志-----------");
            Logger.Warn("警告日志-----------");
            Logger.Trace("跟踪日志-----------");
            Logger.Log(NLog.LogLevel.Warn, "Log日志------------------");

            _logger.LogInformation("你访问了首页");
            _logger.LogWarning("警告信息");
            _logger.LogError("错误信息");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
