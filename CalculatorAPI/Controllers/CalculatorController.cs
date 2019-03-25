using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using Calculator;
using CalculatorAPI.Models;
using CalculatorAPI.Models.Request;
using CalculatorAPI.Models.Response;
using Ninject;


namespace CalculatorAPI.Controllers
{
    /// <summary>
    /// Calculator API that performs all of the 4 calculator operations by calling the simple calculator class
    /// </summary>
    [RoutePrefix("api/v1/Calculator")]
    public class CalculatorController : ApiController
    {
        private readonly ISimpleCalculator _simpleCalculator;
        private readonly IDiagnostics _diagnostics;
        private readonly IDummyDiagnostics _dummyDiagnostics;

        
        public CalculatorController([Named("DbStoredProc")] IDiagnostics diagnostics, IDummyDiagnostics dummyDiagnostics)
        {
            _diagnostics = diagnostics;
            _dummyDiagnostics = dummyDiagnostics;
            _simpleCalculator = new SimpleCalculator(_diagnostics,_dummyDiagnostics);
        }
        
        /// <summary>
        /// Add Operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CalculatorResponse))]
        [Route("add")]
        public IHttpActionResult Add(CalculatorRequest model)
        {
            int x, y;
            CalculatorResponse response = null;
            
            if (int.TryParse(model.NumberOne.ToString(), out x) == false)
                ModelState.AddModelError(nameof(model.NumberOne), "NumberOne must be a number.");


            if (int.TryParse(model.NumberTwo.ToString(), out y) == false)
                ModelState.AddModelError(nameof(model.NumberTwo), "NumberTwo must be a number.");

            if (ModelState.IsValid)
            {
               var  result = _simpleCalculator.Add(model.NumberOne, model.NumberTwo);
                response = new CalculatorResponse { Result = result };
            }
            return Ok(response);
        }
        /// <summary>
        /// Subtract Operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof (CalculatorResponse))]
        [Route("subtract")]
        public IHttpActionResult Subtract(CalculatorRequest model)
         {
            int x, y;
            CalculatorResponse response = null;

            if (int.TryParse(model.NumberOne.ToString(), out x) == false)
                ModelState.AddModelError(nameof(model.NumberOne), "NumberOne must be a number.");


            if (int.TryParse(model.NumberTwo.ToString(), out y) == false)
                ModelState.AddModelError(nameof(model.NumberTwo), "NumberTwo must be a number.");

            if (ModelState.IsValid)
            {
                var result = _simpleCalculator.Subtract(model.NumberOne, model.NumberTwo);
                response = new CalculatorResponse { Result = result };
            }
            return Ok(response);
        }

        /// <summary>
        /// Multiply operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CalculatorResponse))]
        [Route("multiply")]
        public IHttpActionResult Multiply(CalculatorRequest model)
        {
            int x, y;
            CalculatorResponse response = null;

            if (int.TryParse(model.NumberOne.ToString(), out x) == false)
                ModelState.AddModelError(nameof(model.NumberOne), "NumberOne must be a number.");


            if (int.TryParse(model.NumberTwo.ToString(), out y) == false)
                ModelState.AddModelError(nameof(model.NumberTwo), "NumberTwo must be a number.");

            if (ModelState.IsValid)
            {
                var result = _simpleCalculator.Multiply(model.NumberOne, model.NumberTwo);
                response = new CalculatorResponse { Result = result };
            }
            return Ok(response);
        }
        /// <summary>
        /// Divide Operation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CalculatorResponse))]
        [Route("divide")]
        public IHttpActionResult Divide(CalculatorRequest model)
        {
            int x, y;
            CalculatorResponse response = null;

            if (int.TryParse(model.NumberOne.ToString(), out x) == false)
                ModelState.AddModelError(nameof(model.NumberOne), "NumberOne must be a number.");


            if (int.TryParse(model.NumberTwo.ToString(), out y) == false)
                ModelState.AddModelError(nameof(model.NumberTwo), "NumberTwo must be a number.");

            if (ModelState.IsValid)
            {
                var result = _simpleCalculator.Divide(model.NumberOne, model.NumberTwo);
                response = new CalculatorResponse { Result = result };
            }
            return Ok(response);
        }


    }
}
