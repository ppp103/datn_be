using datn.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datn.Application
{
    public class GetTopicTreeQuery : IRequest<List<TopicTreeDto>>
    {
        //public string? Name { get; set; }

        //public int? ParentId { get; set; }

        //public short? SortDescending { get; set; } = 1;
    }
}
