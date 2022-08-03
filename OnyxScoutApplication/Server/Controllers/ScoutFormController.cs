using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnyxScoutApplication.Server.Data;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Controllers
{
    [OnyxAuthorize(Role = Role.Scouter)]
    [ApiController]
    [Route("[controller]")]
    public class ScoutFormController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly IScoutFormUnitOfWork unitOfWork;
        private readonly AmazonS3Client amazonS3Client;

        public ScoutFormController(IWebHostEnvironment env, IScoutFormUnitOfWork unitOfWork, AmazonS3Client amazonS3Client)
        {
            this.env = env;
            this.unitOfWork = unitOfWork;
            this.amazonS3Client = amazonS3Client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDto>>> Get()
        {
            return await unitOfWork.ScoutForms.GetAll();
        }
        
        [HttpGet("GetAllByType/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> Get(ScoutFormType scoutFormType)
        {
            return await unitOfWork.ScoutForms.GetAllByType(scoutFormType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormDto>> Get(string id)
        {
            var result = await unitOfWork.ScoutForms.GetWithFields(id);
            return result;
        }

        [HttpPost("SaveImage/{teamNumber:int}/{keyName}")]
        public async Task<ActionResult> SaveImage(int teamNumber, string keyName, [FromForm] IEnumerable<IFormFile> files)
        {
            var form = await unitOfWork.ScoutForms.GetByTeamAndKey(teamNumber, keyName, ScoutFormType.Pit);
            if (form.Value == null)
            {
                return form.Result;
            }
            var file = files.ElementAt(0);
            var untrustedFileName = file.FileName;
            var trustedFileNameForDisplay =
                WebUtility.HtmlEncode(untrustedFileName);
            if (file.Length == 0)
            {
                return new BadRequestObjectResult($"{trustedFileNameForDisplay} length is 0");
            }
            // if (file.Length > maxFileSize)
            // {
            //     return new BadRequestObjectResult($"{trustedFileNameForDisplay} of {file.Length} bytes is " +
            //                                       $"larger than the limit of {maxFileSize} bytes");
            // }
            try
            {

                var fileName = form.Value.TeamNumber + form.Value.KeyName + Path.GetExtension(file.FileName);
                var path = Path.Combine(env.ContentRootPath, "Images");
                //var path = Path.Combine("/public", "Images");
                Directory.CreateDirectory(path);
                path = Path.Combine(path, fileName);
                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);
                await fs.DisposeAsync();
                var subDir = Environment.GetEnvironmentVariables()["CLOUD_CUBE-SUB_DIR"]!.ToString();
                var bucketName = Environment.GetEnvironmentVariables()["CLOUD_CUBE-BUCKET_NAME"]!.ToString();
                var success = await S3Bucket.UploadFileAsync(amazonS3Client, bucketName,subDir! + "/" + fileName, path);
                if (!success)
                {
                    return Problem($"Could not add file to bucket");
                }
                var url = Environment.GetEnvironmentVariables()["CLOUD_CUBE-URL"]!.ToString();

                form.Value.ImageName = url! + "/" + subDir + "/" + fileName;
                form.Value.ImageFileName = fileName;
                form.Value.IsImageUploaded = true;
                await unitOfWork.ScoutForms.UpdateFromTracking(form.Value);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error saving file on server: " + ex);
                return Problem($"Could not add file to server");
            }
            return new OkResult();
        }

        [HttpGet("GetAllByEvent/{eventKey}")]
        [HttpGet("GetAllByEvent/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>>
            GetAllByEvent(string eventKey, ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByEvent(eventKey, scoutFormType);
        }
        
        [HttpGet("GetAllByEventWithData/{eventKey}")]
        [HttpGet("GetAllByEventWithData/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey,
            ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByEventWithData(eventKey, scoutFormType);
        }

        //[HttpGet("ByYear/{year}")]
        //public async Task<ActionResult<ScoutFormDto>> GetByYear(int year)
        //{
        //    return await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScoutForm(string id, [FromBody] FormDto formModel)
        {
            if (!User.IsInRole(Role.Admin.ToString()) && User.GetDisplayName() != formModel.WriterUserName)
            {
                return new UnauthorizedObjectResult($"Only {Role.Admin.ToString()} or" +
                                                    $" {formModel.WriterUserName} can edit this form!");
            }
            var response = await unitOfWork.ScoutForms.Update(id, formModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutForm([FromBody] FormDto formModel)
        {
            if (!ModelState.IsValid) 
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "Invalid inputs!");
            
            var response = await unitOfWork.ScoutForms.Add(formModel);
            await unitOfWork.Complete();
            return response;

        }

        [HttpGet("GetAllByTeam/{teamNumber}/{eventKey}")]
        [HttpGet("GetAllByTeam/{teamNumber}/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeam(int teamNumber, string eventKey,
            ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey, scoutFormType);
        }
    }
}
