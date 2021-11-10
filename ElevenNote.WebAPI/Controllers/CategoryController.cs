using ElevenNote.Models.CategoryModels;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoryController : ApiController
    {
        [Route]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var svc = CreateCategoryService();
            if (svc is null) return Unauthorized();
            return Ok(svc.GetCategories());
        }

        [Route("GetBy/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var svc = CreateCategoryService();
            if (svc is null) return Unauthorized();
            var cat = svc.GetCategoryById(id);
            if (cat is null) return BadRequest($"No category with id of {id} exists in the context");
            return Ok(svc.GetCategoryById(id));
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CreateCategoryService().CreateCategory(category))
                return InternalServerError();

            return Ok();
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CreateCategoryService().UpdateCategory(category))
                return InternalServerError();

            return Ok();
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!CreateCategoryService().DeleteCategory(id))
                    return InternalServerError();
                return Ok();
            }
            catch (UnauthorizedAccessException err)
            {
                return BadRequest(err.Message);

            }
        }

        private CategoryService CreateCategoryService()
        {
            try
            {
                return new CategoryService(Guid.Parse(User.Identity.GetUserId()));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
