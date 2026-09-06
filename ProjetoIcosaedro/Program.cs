// ---------------------------------------------------------------
// Disciplina : Introducao a Computacao Grafica (ICG)
// Projeto    : 3o Bimestre - Geracao do Icosaedro 2D
// Prof.      : Wagner Santos C. de Jesus
// ---------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace ProjetoIcosaedro
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
