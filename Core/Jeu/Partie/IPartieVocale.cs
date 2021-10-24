using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// Interface qui contrôle les parties vocales.
    /// </summary>
    public interface IPartieVocale : IPartie, IEquatable<IPartieVocale>
    {
        ulong ChannelVocal { get; }
    }
}
