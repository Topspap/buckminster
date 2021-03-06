﻿using System;

using Grasshopper.Kernel;
using Buckminster.Types;

namespace Buckminster.Components
{
    public class RibbonComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public RibbonComponent()
            : base("Buckminster's Ribbon Component", "Ribbon",
                "Gives thickness to mesh edges.",
                "Buckminster", "Modify")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new MeshParam(), "Mesh", "M", "Input mesh", GH_ParamAccess.item);
            pManager.AddNumberParameter("Offset", "O", "Distance to offset edges in plane of adjacent faces", GH_ParamAccess.item, 1.0);
            pManager.AddBooleanParameter("Boundaries", "B", "Whether to ribbon boundary edges or not", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Smooth", "S", "Insert extra vertices to preserve shape when subdividing?", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new MeshParam(), "Mesh", "M", "Ribbon mesh", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            if (!DA.GetData(0, ref mesh)) { return; }

            double offset = double.NaN;
            if (!DA.GetData(1, ref offset)) { return; }

            Boolean edges = false;
            if (!DA.GetData(2, ref edges)) { return; }

            Boolean smooth = false;
            if (!DA.GetData(3, ref smooth)) { return; }

            float smooth_val;
            if (smooth)
                smooth_val = 0.1f;
            else
                smooth_val = 0.0f;

            DA.SetData(0, mesh.Ribbon((float) offset, edges, smooth_val));
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{50e8cfb7-5fc4-47e0-b721-9cd6e45861c9}"); }
        }
    }
}