using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _commanderRepo;
        private readonly IMapper _mapper;

        // private readonly MockCommanderRepo _commanderRepo = new MockCommanderRepo();
        public CommandsController(ICommanderRepo commanderRepo, IMapper mapper)
        {
            _commanderRepo = commanderRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var allcommands = _commanderRepo.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(allcommands));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _commanderRepo.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/command/
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<command>(commandCreateDto);
            _commanderRepo.CreateCommand(commandModel);
            _commanderRepo.SaveChanges();

            var commandReaddto = _mapper.Map<CommandReadDto>(commandModel);
            // return Ok(commandReaddto);
            return CreatedAtRoute(nameof(GetCommandById), new { id = commandReaddto.Id }, commandReaddto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandFromRepo);// this syntax because both has data present, previously only source data was available
            //this already upadated our model so no needed implementation in SqlCommanderRepo
            _commanderRepo.UpdateCommand(commandFromRepo);
            
            _commanderRepo.SaveChanges();

            return NoContent();

        }

        //PATCH  api/commands/{id} 
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map( commandToPatch, commandFromRepo);

            _commanderRepo.UpdateCommand(commandFromRepo);

            _commanderRepo.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandFromRepo = _commanderRepo.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }

            _commanderRepo.DeleteCommand(commandFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();
        }



    }
}