﻿using System;
using System.Resources;
using System.Runtime.Remoting.Channels;

namespace Company.CodeJapaneseDefaultSatelliteEn.MultiLangModule
{
    class MultiLangComponent
    {
        public void RunComponent()
        {
            // Access resource by Getter (Recommended as by http://stackoverflow.com/a/14503044/2003325)
            Console.WriteLine(CompText.helloWorld);

            // Alternatie access by declaring a Resource Manager instance (Microsoft Tutorial)
            // Not recommend due to magic strings!
            var locRm = new ResourceManager("Company.CodeJapaneseDefaultSatelliteEn.MultiLangModule.CompText", typeof(MultiLangComponent).Assembly);
            Console.WriteLine(locRm.GetString("helloWorld"));


            //Demo Exception Message
            try
            {
                throw new InsufficientMemoryException(CompText.errorInsuffMemory);
            }
            catch(InsufficientMemoryException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
