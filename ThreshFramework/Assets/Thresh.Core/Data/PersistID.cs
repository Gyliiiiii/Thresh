using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Thresh.Core.Data
{
    public class PersistIDCompare : IEqualityComparer<PersistID>
    {
        public bool Equals(PersistID x, PersistID y)
        {
            return x == y;
        }

        public int GetHashCode(PersistID obj)
        {
            return obj.Self != null ? obj.Self.GetHashCode() : obj.GetHashCode();
        }
    }

    public struct PersistID : IFormattable, IComparable, IComparable<PersistID>, IEquatable<PersistID>
    {
        [DataMember] [ProtoMember(Constant.PERSISTID_PROTO_IDENT)]
        private string _Root;

        [DataMember] [ProtoMember(Constant.PERSISTID_PROTO_SERIAL)]
        private string _Self;

        public static readonly PersistID Empty = new PersistID(Guid.Empty.ToString(), Guid.Empty.ToString());

        public static implicit operator PersistID(Guid guid)
        {
            return PersistID.GenRoot(guid);
        }

        public static implicit operator String(PersistID pid)
        {
            return pid.Self;
        }

        private static PersistID GenRoot(Guid guid)
        {
            return new PersistID(guid, guid);
        }

        private PersistID(Guid root, Guid self)
        {
            _Root = root.ToString();
            _Self = self.ToString();
        }

        private PersistID(string root, Guid self)
        {
            _Root = root;
            _Self = self.ToString();
        }

        private PersistID(string root, string self)
        {
            _Root = root;
            _Self = self;
        }

        private PersistID(Guid root, string self)
        {
            _Root = root.ToString();
            _Self = self;
        }

        private PersistID(string s)
        {
            if (s == "Empty")
            {
                _Root = Guid.Empty.ToString();
                _Self = Guid.Empty.ToString();
            }

            string[] guids = s.Split(Constant.PERSISTID_MEMBERS_SPLIT_FLAG);
            if (guids.Length == 1)
            {
                _Root = new Guid(guids[Constant.PERSISTID_PROTO_IDENT - 1]).ToString();
                _Self = new Guid(guids[Constant.PERSISTID_PROTO_IDENT - 1]).ToString();
            }
            else if (guids.Length == Constant.PERSISTID_MEMBERS_SIZE)
            {
                _Root = new Guid(guids[Constant.PERSISTID_PROTO_IDENT - 1]).ToString();
                _Self = new Guid(guids[Constant.PERSISTID_PROTO_SERIAL - 1]).ToString();
            }
            else
            {
                _Root = Guid.Empty.ToString();
                _Self = Guid.Empty.ToString();
            }
        }

        public String Root
        {
            get
            {
                if (_Root == null)
                {
                    _Root = Guid.Empty.ToString();
                }

                return _Root;
            }
        }

        public String Self
        {
            get
            {
                if (_Self == null)
                {
                    _Self = Guid.Empty.ToString();
                }

                return _Self;
            }
        }

        public static implicit operator PersistID(string guid)
        {
            return new PersistID(guid);
        }

        public static bool operator ==(PersistID pid1, PersistID pid2)
        {
            return pid1.Self == pid2.Self;
        }

        public static bool operator !=(PersistID sid1, PersistID sid2)
        {
            return !(sid1 == sid2);
        }

        public static PersistID Parse(string g)
        {
            try
            {
                return new PersistID(g);
            }
            catch
            {
                return Empty;
            }
        }

        public static PersistID Parse(string g, PersistID root)
        {
            try
            {
                PersistID pid = new PersistID(g);
                pid._Root = root.Root;
                return pid;
            }
            catch
            {
                return Empty;
            }
        }

        public static PersistID New()
        {
            Guid guid = Guid.NewGuid();
            PersistID pid = new PersistID(guid, guid);
            return pid;
        }

        public bool IsRoot
        {
            get { return Root == Self; }
        }

        public bool IsEmpty(PersistID pid)
        {
            return pid == Empty;
        }

        public PersistID Spawn()
        {
            Guid serial = Guid.NewGuid();
            
            PersistID new_pid = new PersistID(Root, serial);
            
            return new_pid;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("{0}", Self == null ? Guid.Empty.ToString() : Self);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return Self.CompareTo(obj);
        }

        public int CompareTo(PersistID other)
        {
            return Self.CompareTo(other.Self);
        }

        public bool Equals(PersistID other)
        {
            return Self.Equals(other.Self);
        }
    }
}