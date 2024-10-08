﻿using Azure;
using Bloggy.Web.Data;
using Bloggy.Web.Models.Domain;
using Bloggy.Web.Models.ViewModel;
using Bloggy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest AddTagRequest)

        {
            var tag = new Tag
            {
                DisplayName = AddTagRequest.DisplayName,
                Name = AddTagRequest.Name
            };
            
            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Use DB context to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //var tag = _bloggieDbContext.Tags.Find(Id);
            var tag = await tagRepository.GetAsync(Id); 

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
            return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                //Show success notification
            }
            else
            {
                //Show error notification
            }

            return RedirectToAction("Edit", new { id= editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete (EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }
            //Show an error notification
            return RedirectToAction("Edit" , new { id= editTagRequest.Id });

        }

    }
}
