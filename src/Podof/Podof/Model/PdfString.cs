// <copyright file="PdfString.cs" company="Sage Group PLC">
// Copyright (c) Sage Group PLC. All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace Podof.Model
{
    public class PdfString : PdfValue
    {
        public PdfString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or empty.", nameof(value));
            }

            if (value.StartsWith(Marker))
            {
                value = value.Skip(1).ToString();
            }

            this.Value = value;
        }

        public string Value { get; }  

        public static char Marker => '/';

        public override string ToString() => $"{Marker}{this.Value}";
    }
}
