﻿using MediatR;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
