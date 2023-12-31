﻿using System;
using System.Collections.Generic;

namespace Thresh.Core.Config.INI
{
    /// <summary>
    /// Represents the base class of all elements
    /// that exist in a <see cref="Configuration"/>,
    /// for example sections and settings.
    /// </summary>
    public abstract class ConfigurationElement
    {
        private string mName;
        private Comment? mComment;
        internal List<Comment> mPreComments;

        internal ConfigurationElement(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            mName = name;
        }

        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>
        /// Gets or sets the comment of this element.
        /// </summary>
        public Comment? Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }

        /// <summary>
        /// Gets the list of comments above this element.
        /// </summary>
        public List<Comment> PreComments
        {
            get
            {
                if (mPreComments == null)
                    mPreComments = new List<Comment>();

                return mPreComments;
            }
        }
    }
}