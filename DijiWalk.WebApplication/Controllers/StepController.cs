//-----------------------------------------------------------------------
// <copyright file="StepController.cs" company="DijiWalk">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace DijiWalk.WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DijiWalk.Common.Contracts;
    using DijiWalk.Common.Response;
    using DijiWalk.Entities;
    using DijiWalk.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for the Step
    /// </summary>
    [Route("api/[controller]"), ApiController]
    public class StepController : Controller
    {
        /// <summary>
        /// Object private StepRepository with which we will interact with the database
        /// </summary>
        private readonly IStepRepository _repository;

        private readonly IVision _vision;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepController" /> class.
        /// </summary>
        /// <param name="repository">the repository that will interact with the data</param>
        public StepController(IStepRepository repository, IVision vision)
        {
            this._repository = repository;
            _vision = vision;
        }

        /// <summary>
        /// Method to get an Step with his Id
        /// </summary>
        /// <param name="id">Id of the Step</param>
        /// <returns>A Step</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return this.Ok(await this._repository.Find(id));
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to get all Step 
        /// </summary>
        /// <returns>A list of Step</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var step = await this._repository.FindAll();
                return Ok(step.Select(g =>
                {
                    g.Missions = g.Missions.Select(m =>
                    {
                        m.Step = null;
                        m.Trials = m.Trials.Select(t =>
                        {
                            t.Answers = new HashSet<Answer>();
                            t.TeamAnswers = new HashSet<TeamAnswer>();
                            t.Mission = null;
                            t.Type = null;
                            t.CorrectAnswer = null;
                            return t;
                        }).ToList();
                        return m;
                    }).ToList();
                    g.RouteSteps = g.RouteSteps.Select(rs =>
                    {
                        rs.Route.Games = new HashSet<Game>();
                        rs.Route.RouteSteps = new HashSet<RouteStep>();
                        rs.Route.RouteTags = new HashSet<RouteTag>();
                        rs.Route.Organizer = null;
                        return rs;
                    }).ToList();
                    g.StepTags = g.StepTags.Select(st =>
                    {
                        st.Step = null;
                        st.Tag.StepTags = new HashSet<StepTag>();
                        st.Tag.RouteTags = new HashSet<RouteTag>();
                        return st;
                    }).ToList();
                    return g;
                }).ToList());
            }
            catch (Exception e)
            {
                return this.StatusCode(500, e);
            }
        }

        /// <summary>
        /// Method to Add a Step 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Step step)
        {
            try
            {
                return this.Ok(await _repository.Add(step));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Update a Step 
        /// </summary>
        /// <returns>A Step</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Step step)
        {
            try
            {
                step.Id = id;
                return this.Ok(await _repository.Update(step));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        /// <summary>
        /// Method to Delete a Step 
        /// </summary>
        /// <returns>Ok Object</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return this.Ok(await this._repository.Delete(id));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] Validate validate)
        {
            try
            {
                return this.Ok(await this._repository.Validate(validate));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] Validate validate)
        {
            try
            {
                return this.Ok(await this._repository.Check(validate));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }

        [HttpGet, Route("test")]
        public async Task<IActionResult> test()
        {
            try
            {

                return this.Ok(await this._repository.Test("https://00e9e64bacf823a5e9fa6c8c8892d4084caa844c0aac596752-apidata.googleusercontent.com/download/storage/v1/b/dijiwalk-test/o/MicrosoftTeams-image.png?qk=AD5uMEu6C7GAVBlOKnJNU6K9RXnO2G8PR3ImoTnf0NMLjtMuIIraanKKz63_vzyr0AB9mb8YRoPt193f2IyggFlrViVAJpupVYirHSbDwBO9nRFGRR0VfBes8uDv1RlKu8d4ZKylFlhuBNwXYOLfYJzZvUCaPqegK3p0o0pM-dBa2tAWcaCzjSX21fJPZr1BDByaJDeJUX7pirTQExsYRmUzTAwU3QrXvLXfyb71Pz7iK1WJ1JKNMPUzIUnL2gYqi1TbbZIbTHMSJg_SZGQdBvcn2SOwYwhksOZ5rko_zzKsT2RGiNPxxZ1tlWqqHqtvbzxiIFstRe6uycoVm3gxuBi3t7AeEDtjyhTofmzGc1AiDUCMCbLC4XQM_3wdYV4IOB8iMJXII8D1HKZrSHvGDNlrYYgq4FX6ezqrE-FSDHII-fWy6EJKZ-ILipXd2jAH1144EDg5UTdCvVlL-WBe6-u0C4FwYffb9o5cbiFZxmVVue6tG-4w-aVnqOFB0m169mtbd3foXfXxP55U6r7kBBI3t0Qnt6hdSWguYpwLIy7jCVUL05-4cayIErL39o-1oeL-fVp9z2fCKWn1E0fYpgK4oR3gEofpXJQrF76khFJpsqBgC7yCdeOZuvinTLUO5pf-hEcUUl4_x1L5in4MyWuabNlVLTkvlu-gXrgJrd9F7yYJbR-zQiRyZsPIeZUrjHwI7nP9cO41syEmY7drTCd9QwREStyi-zete2lC-JDVaeLL8M7SD2gbLQ_MWsPMkOm1lbcF-WekWf8zc1i7K6uKxrs05tXTcYwWg3mUDQbYu6JXFNnAsNA"));
            }
            catch (Exception e)
            {
                return this.Ok(TranslateError.Convert(e));
            }
        }



    }




}