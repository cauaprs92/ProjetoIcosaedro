// ---------------------------------------------------------------------------
//  Universidade do Vale do Paraiba - Colegio Tecnico Antonio Teixeira Fernandes
//  Disciplina : Introducao a Computacao Grafica (ICG)
//  Professor  : Wagner Santos C. de Jesus
//
//  PRIMITIVAS GRAFICAS
//
//  Biblioteca de primitivas do projeto. Todo o desenho do icosaedro e do
//  mosaico de cores e feito EXCLUSIVAMENTE atraves destas funcoes.
// ---------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjetoIcosaedro
{
    public partial class Form1 : Form
    {
        // ===================================================================
        //  1) OBJETOS BASICOS : COR, CANETA, PINCEL, HACHURA E FONTE
        // ===================================================================

        // Material II - Objeto da classe Color
        //   cor = Color.FromArgb(<R>,<G>,<B>);
        public Color cor(int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }

        // Material II - Objeto da classe Color com canal Alfa (transparencia)
        //   cor = Color.FromArgb(<Alfa>,<R>,<G>,<B>);
        public Color cor(int a, int r, int g, int b)
        {
            return Color.FromArgb(a, r, g, b);
        }

        // Material II / IV - Objeto da classe Pen
        //   Pen <Objeto> = new Pen(<cor>,<ExpN>);   <ExpN> = espessura
        public Pen caneta(int r, int g, int b, int esp)
        {
            return new Pen(cor(r, g, b), esp);
        }

        // Material IV - Caneta com padrao de tracejado (DashPattern)
        public Pen canetaTracejada(int r, int g, int b, int esp, float[] padrao)
        {
            Pen p = new Pen(cor(r, g, b), esp);
            p.DashPattern = padrao;
            return p;
        }

        // Material VI - Preenchimento solido (SolidBrush)
        //   SolidBrush fundo = new SolidBrush(Color.FromArgb(0,0,255));
        public SolidBrush pincel(int r, int g, int b)
        {
            return new SolidBrush(cor(r, g, b));
        }

        public SolidBrush pincel(Color c)
        {
            return new SolidBrush(c);
        }

        // Material VI - Preenchimento com hachura (HatchBrush)
        //   HatchBrush preech = new HatchBrush(<TipoH>,<CorL>,<CorF>);
        //   necessita do pacote using System.Drawing.Drawing2D;
        public HatchBrush hachura(HatchStyle tipo, Color corLinha, Color corFundo)
        {
            return new HatchBrush(tipo, corLinha, corFundo);
        }

        // Material V - Objeto da classe Font
        //   Font oFont = new Font(<ExpS>,<ExpN>,<oStyle>);
        public Font fonte(string nome, int tam, FontStyle estilo)
        {
            return new Font(nome, tam, estilo);
        }

        // ===================================================================
        //  2) PONTO (PIXEL)
        // ===================================================================

        // Material II - Criacao de um ponto no video
        //   e.Graphics.DrawLine(<ObjPen>, x, y, x+1, y);
        public void pintaP(PaintEventArgs e, Pen caneta, int x, int y)
        {
            e.Graphics.DrawLine(caneta, x, y, x + 1, y);
        }

        // Material VI - Classe Point : encapsula um par ordenado (coluna,linha)
        //   Point <objeto> = new Point(<X>,<Y>);
        public Point ponto(int x, int y)
        {
            return new Point(x, y);
        }

        // ===================================================================
        //  3) SEGMENTOS DE RETA
        // ===================================================================

        // Material III - Algoritmo DDA (Digital Diferencial Analyser)
        //
        //   dx <- x1 - x0 ; dy <- y1 - y0
        //   se (dx > dy) entao s <- dx senao s <- dy
        //   xi <- dx/s ; yi <- dy/s
        //   para i de 0 ate s faca x <- x + xi ; y <- y + yi ; Pintap(x,y,cor)
        public void retaDDA(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)
        {
            double dx = x1 - x0;
            double dy = y1 - y0;
            double s;

            if (Math.Abs(dx) > Math.Abs(dy))
                s = Math.Abs(dx);
            else
                s = Math.Abs(dy);

            if (s == 0)
            {
                pintaP(e, caneta, x0, y0);
                return;
            }

            double xi = dx / s;
            double yi = dy / s;
            double x = x0;
            double y = y0;

            pintaP(e, caneta, (int)x, (int)y);
            for (int i = 0; i < (int)s; i++)
            {
                x = x + xi;
                y = y + yi;
                pintaP(e, caneta, (int)Math.Round(x), (int)Math.Round(y));
            }
        }

        // Material III / IV - Algoritmo de Breseham
        //
        //   dx = |x1-x0| ; sx = x0 < x1 ? 1 : -1
        //   dy = -|y1-y0|; sy = y0 < y1 ? 1 : -1
        //   err = dx + dy
        //   enquanto verdadeiro
        //      pintap(x0,y0,cor)
        //      se (x0 == x1 e y0 == y1) pare
        //      e2 = 2 * err
        //      se (e2 >= dy) { err += dy ; x0 += sx }
        //      se (e2 <= dx) { err += dx ; y0 += sy }
        public void retaBreseham(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int px = (x0 < x1) ? 1 : -1;
            int dy = -Math.Abs(y1 - y0);
            int py = (y0 < y1) ? 1 : -1;
            int err = dx + dy;

            while (true)
            {
                pintaP(e, caneta, x0, y0);
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 >= dy) { err = err + dy; x0 = x0 + px; }
                if (e2 <= dx) { err = err + dx; y0 = y0 + py; }
            }
        }

        // Material IV - Metodo DrawLine() ja implementado na linguagem
        //   DrawLine(<Cor>, x0,y0,x1,y1);
        public void linha(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)
        {
            e.Graphics.DrawLine(caneta, x0, y0, x1, y1);
        }

        // ===================================================================
        //  4) RETANGULOS
        // ===================================================================

        // Material IV - DrawRectangle(Pen,<Coluna>,<Linha>,<Largura>,<Altura>)
        public void retangulo(PaintEventArgs e, Pen caneta, int x, int y, int larg, int alt)
        {
            e.Graphics.DrawRectangle(caneta, x, y, larg, alt);
        }

        // Material V - Problema proposto : o quadrado como primitiva derivada
        //   do retangulo (largura = altura = lado).
        public void quadrado(PaintEventArgs e, Pen caneta, int x, int y, int lado)
        {
            retangulo(e, caneta, x, y, lado, lado);
        }

        // Material VI - Retangulo preenchido, construido pela primitiva de
        //   poligonos (Point[] + FillPolygon).
        public void retanguloCheio(PaintEventArgs e, Brush pincel, int x, int y, int larg, int alt)
        {
            int[] vx = { x, x + larg, x + larg, x };
            int[] vy = { y, y, y + alt, y + alt };
            preenchePoligono(e, pincel, poligono(vx, vy, 4));
        }

        // ===================================================================
        //  5) CIRCULOS, ELIPSES E ARCOS
        // ===================================================================

        // Material VI - Arco/circulo por coordenadas polares (funcoes trigonometricas)
        //
        //   dArc(Xc,Yc,raio,Ti,Tf,cor)
        //      para teta de Ti ate Tf faca
        //         x <- Xc + raio * cos(teta)
        //         y <- Yc + raio * sin(teta)
        //         Pintap(x,y,cor)
        public void arco(PaintEventArgs e, Pen caneta, int xc, int yc, int raio, int ti, int tf)
        {
            for (int t = ti; t <= tf; t++)
            {
                int x = polarX(xc, raio, t);
                int y = polarY(yc, raio, t);
                pintaP(e, caneta, x, y);
            }
        }

        public void circulo(PaintEventArgs e, Pen caneta, int xc, int yc, int raio)
        {
            arco(e, caneta, xc, yc, raio, 0, 360);
        }

        // Material VI - Ponto medio para elipse ja implementado em C#
        //   e.Graphics.DrawEllipse(Pen, Xc, Yc, Lx, Ay);
        public void elipse(PaintEventArgs e, Pen caneta, int x, int y, int lx, int ay)
        {
            e.Graphics.DrawEllipse(caneta, x, y, lx, ay);
        }

        public void elipseCheia(PaintEventArgs e, Brush pincel, int x, int y, int lx, int ay)
        {
            e.Graphics.FillEllipse(pincel, x, y, lx, ay);
        }

        // ===================================================================
        //  6) POLIGONOS
        // ===================================================================

        // Material VI - Construtor de poligonos a partir dos vetores x e y
        //
        //   int[] x = {100,200,200,100}; int[] y = {100,100,300,300};
        //   Point[] pontos = new Point[4];
        //   for(int i=0;i<=3;i++){ Point point1 = new Point(x[i],y[i]); pontos[i]=point1; }
        public Point[] poligono(int[] x, int[] y, int n)
        {
            Point[] pontos = new Point[n];
            for (int i = 0; i <= n - 1; i++)
            {
                Point point1 = ponto(x[i], y[i]);
                pontos[i] = point1;
            }
            return pontos;
        }

        // Material VI - DrawPolygon() : contorno do poligono
        public void desenhaPoligono(PaintEventArgs e, Pen caneta, Point[] pontos)
        {
            e.Graphics.DrawPolygon(caneta, pontos);
        }

        // Material VI - FillPolygon() : preenchimento do poligono
        //   SolidBrush fundo = new SolidBrush(cor);
        //   e.Graphics.FillPolygon(fundo, pontos);
        public void preenchePoligono(PaintEventArgs e, Brush pincel, Point[] pontos)
        {
            e.Graphics.FillPolygon(pincel, pontos);
        }

        // Poligono desenhado aresta a aresta pelos algoritmos DDA / Breseham
        public void poligonoDDA(PaintEventArgs e, Pen caneta, Point[] pontos)
        {
            for (int i = 0; i <= pontos.Length - 1; i++)
            {
                int j = (i + 1) % pontos.Length;
                retaDDA(e, caneta, pontos[i].X, pontos[i].Y, pontos[j].X, pontos[j].Y);
            }
        }

        public void poligonoBreseham(PaintEventArgs e, Pen caneta, Point[] pontos)
        {
            for (int i = 0; i <= pontos.Length - 1; i++)
            {
                int j = (i + 1) % pontos.Length;
                retaBreseham(e, caneta, pontos[i].X, pontos[i].Y, pontos[j].X, pontos[j].Y);
            }
        }

        // Material VI - Algoritmo de ponto dentro / fora de um poligono.
        //   O ponto (x,y) esta dentro do triangulo quando permanece sempre do
        //   mesmo lado das tres retas que formam os seus lados.
        public bool pontoDentro(int x, int y, Point a, Point b, Point c)
        {
            double d1 = lado(x, y, a, b);
            double d2 = lado(x, y, b, c);
            double d3 = lado(x, y, c, a);

            bool neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(neg && pos);
        }

        public double lado(int x, int y, Point p1, Point p2)
        {
            return (double)(p2.X - p1.X) * (y - p1.Y) - (double)(p2.Y - p1.Y) * (x - p1.X);
        }

        // ===================================================================
        //  7) TEXTO GRAFICO
        // ===================================================================

        // Material V - DrawString(<Texto>,<oFont>,<oCor>,x,y)
        public void texto(PaintEventArgs e, string str, Font oFont, Brush pincel, int x, int y)
        {
            e.Graphics.DrawString(str, oFont, pincel, (float)x, (float)y);
        }

        // Versao reduzida : monta a fonte e o pincel internamente
        public void texto(PaintEventArgs e, string str, int x, int y, int tam,
                          FontStyle estilo, Color c)
        {
            Font oFont = fonte("Arial", tam, estilo);
            SolidBrush corletra = pincel(c);
            texto(e, str, oFont, corletra, x, y);
            corletra.Dispose();
            oFont.Dispose();
        }

        // ===================================================================
        //  8) TRANSFORMACOES GEOMETRICAS 2D  (Material V)
        // ===================================================================

        // Conversao de graus para radianos (teta)
        public double radiano(double graus)
        {
            return graus * Math.PI / 180.0;
        }

        // Translacao :  x' = x + tx   /   y' = y + ty
        public int translada(int v, int t)
        {
            return v + t;
        }

        // Escala :  x' = x * sx   /   y' = y * sy
        public double escala(double v, double s)
        {
            return v * s;
        }

        // Rotacao / coordenadas polares :  x' = Xc + r * cos(teta)
        public int polarX(int xc, double raio, double graus)
        {
            return (int)(xc + raio * Math.Cos(radiano(graus)));
        }

        // Rotacao / coordenadas polares :  y' = Yc + r * sen(teta)
        public int polarY(int yc, double raio, double graus)
        {
            return (int)(yc + raio * Math.Sin(radiano(graus)));
        }
    }
}
