using Azure;
using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public GetAllProjectQuery(string queryString, int page, int size)
        {
            QueryString = queryString;
            Page = page;
            Size = size;
        }

        public string QueryString { get; private set; }

        public int Page { get; private set; }

        public int Size { get; private set; }

    }
}
