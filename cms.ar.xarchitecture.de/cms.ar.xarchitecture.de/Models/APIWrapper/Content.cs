using cms.ar.xarchitecture.de.cmsXARCH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace cms.ar.xarchitecture.de.Models
{
    public class Content
    {
        public List<Asset> Assets;
        public List<Anchor> Anchors;
        public List<Scene> Scenes;

        public Content()
        {
            Assets = new List<Asset>();
            Anchors = new List<Anchor>();
            Scenes = new List<Scene>();
        }
    }
}
