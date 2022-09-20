﻿using Core;
using Serialization.Abstractions;
using System.Runtime.Serialization;
using System.Xml;

namespace Xml
{
    public class CustomXmlSerializer : ITraceResultSerializer
    {
        private readonly DataContractSerializer _xmlConverter;
        private readonly XmlWriterSettings _xmlWriterSettings;

        public CustomXmlSerializer()
        {
            _xmlConverter = new DataContractSerializer(typeof(TraceResult));
            _xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            };
        }

        public string Format => "xml";

        public void Serialize(TraceResultDto traceResult, Stream to)
        {
            using var xmlWriter = XmlWriter.Create(to, _xmlWriterSettings);
            _xmlConverter.WriteObject(xmlWriter, traceResult);
        }
    }
}