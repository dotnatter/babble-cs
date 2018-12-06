﻿using System;
using System.Collections.Generic;
using System.Linq;
using Babble.Core.Common;
using Babble.Core.Util;

namespace Babble.Core.PeersImpl
{
    public class Peer
    {
        public int ID { get; set; }
        public string NetAddr { get; set; }
        public string PubKeyHex { get; set; }

        public static Peer New(string pubKeyHex, string netAddr)
        {
            var peer = new Peer {PubKeyHex = pubKeyHex, NetAddr = netAddr};
            peer.ComputeId();

            return peer;
        }

        private byte[] PubKeyBytes()
        {
            return PubKeyHex.Skip(2).ToString().FromHex();
        }

        public void ComputeId()
        {
            // TODO: Use the decoded bytes from hex
            var pubKey = PubKeyBytes();

            var hash = new Fnv1a32();
            hash.ComputeHash(pubKey);
            ID = BitConverter.ToInt32(hash.Hash, 0);
        }


        // ExcludePeer is used to exclude a single peer from a list of peers.
        public static (int, Peer[]) ExcludePeer(Peer[] peers, string peer)
        {
            var index = -1;
            var otherPeers = new List<Peer>();

            int i = 0;
            foreach (var p in peers)
            {
                if (p.NetAddr != peer)
                {
                    otherPeers.Add(p);
                } else
                {
                    index = i;
                }

                i++;
            }

            return (index, otherPeers.ToArray());
        }

    }


}