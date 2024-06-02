using datn.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class UpdateImgCommand : IRequest<UpdatePasswordResponse>
    {
        public int Id { get; set; }
        //public string ImgLink { get; set; }
        public IFormFile? File { get; set; }
    }
}
