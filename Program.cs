using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;

namespace UartMonitor {
    class Program {
        static void Main(string[] args) {
            if (args.Length > 0) {
                SerialPort p = new SerialPort(args[0], 57600);
                p.Open();
                int i = 0;
                byte[] xs = new byte[16];
                int bi = 0;
                for (; ; ) {
                    if (0 == (i % 8)) {
                        Console.Write(" ");
                    }
                    if (0 == (i % 16)) {
                        if (i >= 16) {
                            Console.Write("|");
                            for (int j = 0; j < 16; j++) {
                                byte b = xs[j];
                                char c = Convert.ToChar(xs[j]);
                                if ((b & 0x80) != 0) {
                                    Console.Write(string.Format("."));
                                } else if (char.IsLetterOrDigit(c)) {
                                    Console.Write(string.Format("{0}", c));
                                } else if (b == 0x0d || b == 0x0a) {
                                    Console.Write(string.Format("{0}", '.'));
                                } else {
                                    Console.Write(string.Format("{0}", c));
                                }
                            }
                            Console.Write("|");
                        }
                        bi = 0;
                        Console.WriteLine();
                    }
                    int x = p.ReadByte();
                    Console.Write(string.Format("{0:x2} ", x));
                    xs[bi++] = (byte)x;
                    i++;
                }
                p.Close();
            }
        }
    }
}