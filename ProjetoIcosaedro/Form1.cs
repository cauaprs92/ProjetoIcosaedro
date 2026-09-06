// ---------------------------------------------------------------------------
//  Universidade do Vale do Paraiba - Colegio Tecnico Antonio Teixeira Fernandes
//  Disciplina : Introducao a Computacao Grafica (ICG)
//  Professor  : Wagner Santos C. de Jesus
//  Projeto 3o Bimestre : Geracao da projecao 2D de um Icosaedro
//
//  Este arquivo contem apenas a ESTRUTURA do icosaedro e os EVENTOS da janela.
//  Todo o desenho e feito pelas primitivas graficas do arquivo Primitivas.cs
//  (cor, caneta, pincel, pintaP, retaDDA, retaBreseham, retangulo, poligono,
//   preenchePoligono, texto, translada, escala, polarX, polarY ...).
// ---------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProjetoIcosaedro
{
    public partial class Form1 : Form
    {
        // -------------------------------------------------------------------
        // Area de desenho (regiao retangular da janela grafica)
        // -------------------------------------------------------------------
        const int AREAX = 15;      // coluna inicial da area de desenho
        const int AREAY = 15;      // linha inicial da area de desenho
        const int AREAL = 620;     // largura da area de desenho
        const int AREAA = 620;     // altura da area de desenho

        // Centro da figura (Xc,Yc) e raio base do icosaedro
        const int XC = 325;
        const int YC = 325;
        const int RAIO = 270;

        // Razao aurea : o hexagono interno da projecao possui raio = R / 1.618034
        const double FI = 1.618034;

        // -------------------------------------------------------------------
        // Vetores de transformacao geometrica  (Material V)
        //    Translacao : x' = x + tx  ;  y' = y + ty
        //    Escala     : x' = x * sx  ;  y' = y * sy
        //    Rotacao    : x' = Xc + r*cos(teta) ; y' = Yc + r*sen(teta)
        // -------------------------------------------------------------------
        int tx = 0;
        int ty = 0;
        double sx = 1.0;
        double sy = 1.0;
        double teta = 0.0;

        // -------------------------------------------------------------------
        // Estrutura interna do icosaedro
        //    12 vertices : 0..5  = hexagono interno (raio menor)
        //                  6..11 = hexagono externo (raio maior)
        //    30 arestas  e  20 faces (10 visiveis nesta projecao 2D)
        // -------------------------------------------------------------------
        Point[] vertice = new Point[12];

        int[,] aresta = new int[30, 2]
        {
            // triangulo interno superior (vertices do topo do solido)
            {2,0}, {2,4}, {0,4},
            // triangulo interno inferior (vertices da base do solido)
            {1,3}, {1,5}, {3,5},
            // hexagono externo (contorno da figura)
            {7,8}, {7,6}, {9,8}, {9,10}, {11,6}, {11,10},
            // arestas radiais (interno -> externo no mesmo angulo)
            {2,8}, {0,6}, {4,10}, {7,1}, {9,3}, {11,5},
            // arestas diagonais do topo
            {2,7}, {2,9}, {0,7}, {0,11}, {4,9}, {4,11},
            // arestas diagonais da base
            {8,1}, {6,1}, {6,5}, {8,3}, {10,3}, {10,5}
        };

        // Faces triangulares visiveis na projecao 2D, numeradas de 1 a 10
        int[,] face = new int[10, 3]
        {
            {2,0,4},    // 1  - face central
            {2,0,7},    // 2
            {0,4,11},   // 3
            {2,4,9},    // 4
            {2,7,8},    // 5
            {0,7,6},    // 6
            {0,11,6},   // 7
            {4,11,10},  // 8
            {4,9,10},   // 9
            {2,9,8}     // 10
        };

        // Cor atribuida a cada uma das 10 faces visiveis
        Color[] corFace = new Color[10];

        // -------------------------------------------------------------------
        // Mosaico (grid) de cores  -  5 colunas x 4 linhas = 20 cores
        // -------------------------------------------------------------------
        const int MOSX = 660;      // coluna inicial do mosaico
        const int MOSY = 80;       // linha inicial do mosaico
        const int MOSL = 62;       // largura de cada celula
        const int MOSA = 40;       // altura de cada celula
        const int MOSC = 5;        // numero de colunas
        const int MOSLIN = 4;      // numero de linhas

        Color[] paleta = new Color[20];
        int corSelecionada = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            montaPaleta();
            limpaFaces();
        }

        // ===================================================================
        //  ESTRUTURA DO ICOSAEDRO
        // ===================================================================

        // -------------------------------------------------------------------
        // calculaVertices()
        //
        // Os 12 vertices da projecao 2D sao obtidos por COORDENADAS POLARES
        // (primitivas polarX / polarY) sobre dois hexagonos concentricos:
        //
        //     x = Xc + r * cos(teta)      y = Yc + r * sen(teta)
        //
        // Hexagono externo : r = R              angulos 0,60,120,180,240,300
        // Hexagono interno : r = R / 1.618034   (razao aurea do icosaedro)
        //
        // Sobre esses valores sao aplicadas as transformacoes geometricas
        // pelas primitivas escala() e translada().
        // -------------------------------------------------------------------
        private void calculaVertices()
        {
            double rExterno = escala(RAIO, sx);            // x' = x * sx
            double rInterno = escala(RAIO / FI, sy);       // y' = y * sy

            for (int k = 0; k <= 5; k++)
            {
                double ang = k * 60.0 + teta;              // rotacao : teta

                // vertices 0..5 -> hexagono interno
                int xi = translada(polarX(XC, rInterno, ang), tx);
                int yi = translada(polarY(YC, rInterno, ang), ty);
                vertice[k] = ponto(xi, yi);

                // vertices 6..11 -> hexagono externo
                int xe = translada(polarX(XC, rExterno, ang), tx);
                int ye = translada(polarY(YC, rExterno, ang), ty);
                vertice[k + 6] = ponto(xe, ye);
            }
        }

        // Devolve os 3 pontos de uma face (usado no desenho e no clique)
        private Point[] pontosFace(int f)
        {
            Point[] pontos = new Point[3];
            for (int i = 0; i <= 2; i++)
                pontos[i] = vertice[face[f, i]];
            return pontos;
        }

        // -------------------------------------------------------------------
        // Preenchimento das 10 faces visiveis (primitiva preenchePoligono)
        // -------------------------------------------------------------------
        private void desenhaFaces(PaintEventArgs e)
        {
            for (int f = 0; f <= 9; f++)
            {
                SolidBrush fundo = pincel(corFace[f]);
                preenchePoligono(e, fundo, pontosFace(f));
                fundo.Dispose();
            }
        }

        // -------------------------------------------------------------------
        // Desenho das 30 arestas pelos algoritmos DDA / Breseham
        // -------------------------------------------------------------------
        private void desenhaArestas(PaintEventArgs e)
        {
            Pen preta = caneta(0, 0, 0, 2);
            for (int a = 0; a <= 29; a++)
            {
                Point p0 = vertice[aresta[a, 0]];
                Point p1 = vertice[aresta[a, 1]];

                if (rbDDA.Checked)
                    retaDDA(e, preta, p0.X, p0.Y, p1.X, p1.Y);
                else
                    retaBreseham(e, preta, p0.X, p0.Y, p1.X, p1.Y);
            }
            preta.Dispose();
        }

        // -------------------------------------------------------------------
        // Numeracao das faces visiveis (1 a 10) - primitiva texto()
        // O ponto de escrita e o baricentro do triangulo.
        // -------------------------------------------------------------------
        private void desenhaNumeros(PaintEventArgs e)
        {
            for (int f = 0; f <= 9; f++)
            {
                Point[] p = pontosFace(f);
                int cx = (p[0].X + p[1].X + p[2].X) / 3;
                int cy = (p[0].Y + p[1].Y + p[2].Y) / 3;
                string num = Convert.ToString(f + 1);
                texto(e, num, cx - 6, cy - 10, 11, FontStyle.Bold, cor(0, 0, 128));
            }
        }

        // ===================================================================
        //  MOSAICO DE CORES
        // ===================================================================

        private void montaPaleta()
        {
            paleta[0] = cor(255, 0, 0);        // vermelho
            paleta[1] = cor(255, 128, 0);      // laranja
            paleta[2] = cor(255, 255, 0);      // amarelo
            paleta[3] = cor(128, 255, 0);      // verde limao
            paleta[4] = cor(0, 255, 0);        // verde
            paleta[5] = cor(0, 255, 128);      // verde agua
            paleta[6] = cor(0, 255, 255);      // ciano
            paleta[7] = cor(0, 128, 255);      // azul claro
            paleta[8] = cor(0, 0, 255);        // azul
            paleta[9] = cor(128, 0, 255);      // violeta
            paleta[10] = cor(255, 0, 255);     // magenta
            paleta[11] = cor(255, 0, 128);     // rosa
            paleta[12] = cor(128, 64, 0);      // marrom
            paleta[13] = cor(0, 128, 0);       // verde escuro
            paleta[14] = cor(0, 0, 128);       // azul marinho
            paleta[15] = cor(128, 128, 0);     // oliva
            paleta[16] = cor(0, 0, 0);         // preto
            paleta[17] = cor(85, 85, 85);      // cinza escuro
            paleta[18] = cor(170, 170, 170);   // cinza claro
            paleta[19] = cor(255, 255, 255);   // branco
        }

        private void limpaFaces()
        {
            for (int f = 0; f <= 9; f++)
                corFace[f] = cor(245, 245, 245);
        }

        private void desenhaMosaico(PaintEventArgs e)
        {
            Pen borda = caneta(120, 120, 120, 1);
            Pen destaque = caneta(255, 0, 0, 3);

            for (int i = 0; i <= 19; i++)
            {
                int col = i % MOSC;
                int lin = i / MOSC;
                int x = MOSX + col * MOSL;
                int y = MOSY + lin * MOSA;

                SolidBrush fundo = pincel(paleta[i]);
                retanguloCheio(e, fundo, x, y, MOSL - 4, MOSA - 4);
                fundo.Dispose();

                retangulo(e, borda, x, y, MOSL - 4, MOSA - 4);

                if (i == corSelecionada)
                    retangulo(e, destaque, x - 3, y - 3, MOSL + 2, MOSA + 2);
            }

            // Amostra da cor corrente
            int ax = MOSX;
            int ay = MOSY + MOSLIN * MOSA + 10;
            SolidBrush amostra = pincel(paleta[corSelecionada]);
            retanguloCheio(e, amostra, ax, ay, 40, 26);
            amostra.Dispose();
            retangulo(e, borda, ax, ay, 40, 26);
            texto(e, "Cor selecionada", ax + 50, ay + 5, 9, FontStyle.Regular, cor(0, 0, 0));

            borda.Dispose();
            destaque.Dispose();
        }

        // ===================================================================
        //  EVENTO PAINT  (Material II)
        // ===================================================================
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // fundo da area de desenho preenchido com hachura (Material VI)
            HatchBrush grade = hachura(HatchStyle.SmallGrid, cor(232, 238, 245), cor(255, 255, 255));
            retanguloCheio(e, grade, AREAX, AREAY, AREAL, AREAA);
            grade.Dispose();

            // moldura da area de desenho (Material IV)
            Pen moldura = caneta(180, 180, 180, 1);
            retangulo(e, moldura, AREAX, AREAY, AREAL, AREAA);
            moldura.Dispose();

            calculaVertices();

            desenhaFaces(e);
            desenhaArestas(e);

            if (ckNumeros.Checked)
                desenhaNumeros(e);

            desenhaMosaico(e);

            texto(e, "Icosaedro: 12 vertices - 30 arestas - 20 faces (10 visiveis)",
                  AREAX + 8, AREAY + AREAA - 22, 9, FontStyle.Regular, cor(90, 90, 90));
        }

        // ===================================================================
        //  EVENTO MOUSECLICK  (Material IV / VI)
        //    MouseEventArgs -> captura coluna (e.X) e linha (e.Y)
        // ===================================================================
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            // 1) O clique ocorreu sobre uma celula do mosaico de cores ?
            for (int i = 0; i <= 19; i++)
            {
                int col = i % MOSC;
                int lin = i / MOSC;
                int cx = MOSX + col * MOSL;
                int cy = MOSY + lin * MOSA;

                if (x >= cx && x <= cx + MOSL - 4 && y >= cy && y <= cy + MOSA - 4)
                {
                    corSelecionada = i;
                    Invalidate();
                    return;
                }
            }

            // 2) O clique ocorreu sobre uma das 10 faces visiveis ?
            calculaVertices();
            for (int f = 0; f <= 9; f++)
            {
                Point[] p = pontosFace(f);
                if (pontoDentro(x, y, p[0], p[1], p[2]))
                {
                    corFace[f] = paleta[corSelecionada];
                    Invalidate();
                    return;
                }
            }
        }

        // ===================================================================
        //  CONTROLES DE TRANSFORMACAO (TrackBar) - Material V
        // ===================================================================
        private void Controle_Scroll(object sender, EventArgs e)
        {
            tx = tbTransX.Value;                       // vetor de translacao em x
            ty = tbTransY.Value;                       // vetor de translacao em y
            sx = tbEscala.Value / 100.0;               // vetor de escala (sx)
            sy = tbEscala.Value / 100.0;               // vetor de escala (sy)
            teta = tbRotacao.Value;                    // angulo de rotacao

            lblTransX.Text = "Translacao X (tx) = " + Convert.ToString(tx);
            lblTransY.Text = "Translacao Y (ty) = " + Convert.ToString(ty);
            lblEscala.Text = "Escala (sx,sy) = " + sx.ToString("0.00");
            lblRotacao.Text = "Rotacao (teta) = " + Convert.ToString(tbRotacao.Value) + " graus";

            Invalidate();   // nova chamada ao metodo Paint()
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            limpaFaces();
            Invalidate();
        }

        private void btRestaurar_Click(object sender, EventArgs e)
        {
            tbTransX.Value = 0;
            tbTransY.Value = 0;
            tbEscala.Value = 100;
            tbRotacao.Value = 0;
            Controle_Scroll(sender, e);
        }
    }
}
