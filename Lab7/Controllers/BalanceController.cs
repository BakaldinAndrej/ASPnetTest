using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Balance;
using Lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceController : ControllerBase
    {
        [HttpPost]
        public async Task<Responce> PostAsync([FromBody] InputData input)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Проверка аргумента на null
                    _ = input ?? throw new ArgumentNullException(nameof(input));

                    // Решение задачи
                    IBalanceSolver solver = new AccordBalanceSolver();
                    var output = new OutputData
                    {
                        X = solver.Solve(input.X0, input.A, input.B, input.Measurability,
                            input.Tolerance, input.Lower, input.Upper),
                        DisbalanceOriginal = solver.DisbalanceOriginal,
                        Disbalance = solver.Disbalance
                    };

                    return new Responce
                    {
                        Type = "result",
                        Data = output
                    };
                }
                catch (Exception e)
                {
                    return new Responce
                    {
                        Type = "error",
                        Data = e.Message
                    };
                }
            });
        }
    }
}
