using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Hiper.Net.Utilidades
{
    public class UtilRedes
    {
        /// <summary>
        /// Obtiene las direcciones IP de la máquina
        /// JJayo 15/11/2012. Filtramos solo las direcciones IP version 4, pues en w7 nos retornaba IPv6(::1) e IPv4
        /// </summary>
        /// <returns>Retorna un array de tipo IPAddress que contiene las IPv4</returns>
        public static IPAddress[] ObtenerDireccionesIP()
        {
            //IPAddress[] arrDir = Dns.GetHostEntry(Dns.GetHostName()).AddressList; //JJayo 15/11/2012. Obtiene todas las IP's incluidas IPv6

            List<IPAddress> localAddresses = new List<IPAddress>();
 
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork) // filter out ipv4
                {
                    localAddresses.Add(ipAddress);
                }
            }

            IPAddress[] arrDir = new IPAddress[localAddresses.Count];

            int i=0;
            foreach (IPAddress myIP in localAddresses)
            {
                arrDir[i] = myIP;
                i++;
            }
            
            return arrDir;
        }
    }
}