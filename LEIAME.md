# Projeto 3º Bimestre — Introdução à Computação Gráfica (ICG)

**Geração da projeção 2D de um Icosaedro** — C# / Windows Forms (.NET)

Prof. Wagner Santos C. de Jesus — Colégio Técnico Antônio Teixeira Fernandes / UNIVAP

---

## Como abrir e executar

1. Abra `ProjetoIcosaedro.sln` no Visual Studio.
2. Pressione **F5** (Iniciar).

Pela linha de comando:

```bash
dotnet run --project ProjetoIcosaedro/ProjetoIcosaedro.csproj
```

---

## Estrutura dos arquivos

```
ProjetoIcosaedro/
├── ProjetoIcosaedro.sln
└── ProjetoIcosaedro/
    ├── ProjetoIcosaedro.csproj
    ├── Program.cs              -> Main() / Application.Run
    ├── Primitivas.cs           -> BIBLIOTECA DE PRIMITIVAS GRAFICAS
    ├── Form1.cs                -> estrutura do icosaedro + eventos
    └── Form1.Designer.cs       -> TrackBars, RadioButtons, Labels, Buttons
```

`Form1.cs` **não chama nenhum método do objeto `Graphics` diretamente**: todo o
desenho passa pelas primitivas de `Primitivas.cs`.

---

## Biblioteca de primitivas (`Primitivas.cs`)

Todas seguem o mesmo padrão do modelo dado em aula — recebem `PaintEventArgs e`
e os objetos `Pen` / `Brush` já montados pelas funções `cor()`, `caneta()` e
`pincel()`.

### 1. Objetos básicos

| Primitiva | Material |
|---|---|
| `Color cor(int r, int g, int b)` | II |
| `Color cor(int a, int r, int g, int b)` — com canal Alfa | II |
| `Pen caneta(int r, int g, int b, int esp)` | II / IV |
| `Pen canetaTracejada(int r, int g, int b, int esp, float[] padrao)` — `DashPattern` | IV |
| `SolidBrush pincel(int r, int g, int b)` / `pincel(Color c)` | VI |
| `HatchBrush hachura(HatchStyle tipo, Color corLinha, Color corFundo)` | VI |
| `Font fonte(string nome, int tam, FontStyle estilo)` | V |

### 2. Ponto (pixel)

| Primitiva | Material |
|---|---|
| `void pintaP(PaintEventArgs e, Pen caneta, int x, int y)` | II |
| `Point ponto(int x, int y)` | VI |

### 3. Segmentos de reta

| Primitiva | Material |
|---|---|
| `void retaDDA(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)` | III |
| `void retaBreseham(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)` | III / IV |
| `void linha(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)` — `DrawLine` | IV |

### 4. Retângulos

| Primitiva | Material |
|---|---|
| `void retangulo(PaintEventArgs e, Pen caneta, int x, int y, int larg, int alt)` | IV |
| `void quadrado(PaintEventArgs e, Pen caneta, int x, int y, int lado)` | V (problema proposto) |
| `void retanguloCheio(PaintEventArgs e, Brush pincel, int x, int y, int larg, int alt)` | VI |

### 5. Círculos, elipses e arcos

| Primitiva | Material |
|---|---|
| `void arco(PaintEventArgs e, Pen caneta, int xc, int yc, int raio, int ti, int tf)` | VI |
| `void circulo(PaintEventArgs e, Pen caneta, int xc, int yc, int raio)` | VI |
| `void elipse(PaintEventArgs e, Pen caneta, int x, int y, int lx, int ay)` | VI |
| `void elipseCheia(PaintEventArgs e, Brush pincel, int x, int y, int lx, int ay)` | VI |

### 6. Polígonos

| Primitiva | Material |
|---|---|
| `Point[] poligono(int[] x, int[] y, int n)` | VI |
| `void desenhaPoligono(PaintEventArgs e, Pen caneta, Point[] pontos)` | VI |
| `void preenchePoligono(PaintEventArgs e, Brush pincel, Point[] pontos)` | VI |
| `void poligonoDDA(...)` / `void poligonoBreseham(...)` | III / VI |
| `bool pontoDentro(int x, int y, Point a, Point b, Point c)` | VI |

### 7. Texto gráfico

| Primitiva | Material |
|---|---|
| `void texto(PaintEventArgs e, string str, Font oFont, Brush pincel, int x, int y)` | V |
| `void texto(PaintEventArgs e, string str, int x, int y, int tam, FontStyle estilo, Color c)` | V |

### 8. Transformações geométricas 2D

| Primitiva | Equação | Material |
|---|---|---|
| `double radiano(double graus)` | — | VI |
| `int translada(int v, int t)` | `x' = x + tx` | V |
| `double escala(double v, double s)` | `x' = x · sx` | V |
| `int polarX(int xc, double raio, double graus)` | `x' = Xc + r·cos(teta)` | V / VI |
| `int polarY(int yc, double raio, double graus)` | `y' = Yc + r·sen(teta)` | V / VI |

---

## Como usar o programa

| Ação | Resultado |
|---|---|
| Clicar numa cor do mosaico e depois numa face | Pinta aquela face (faces numeradas de 1 a 10) |
| TrackBar **Translação X / Y** | Move a figura (`translada`) |
| TrackBar **Escala** | Redimensiona uniformemente em relação ao centro (`escala`) |
| TrackBar **Rotação** | Gira em torno do centro (`polarX` / `polarY`) |
| **Breseham / DDA** | Troca o algoritmo usado para traçar as 30 arestas |
| **Numerar faces** | Liga/desliga os índices 1..10 |
| **Limpar cores** / **Restaurar transformações** | Reinicia cores ou os TrackBars |

---

## A geometria da figura

A projeção 2D do icosaedro (vista pelo eixo de simetria de ordem 3) é formada por
**dois hexágonos concêntricos e alinhados**:

- **hexágono externo**: raio `R`, vértices em 0°, 60°, 120°, 180°, 240°, 300°;
- **hexágono interno**: raio `R / φ`, com `φ = 1,618034` (razão áurea), nos mesmos ângulos.

Os vértices são gerados pelas primitivas `polarX` / `polarY`:

```
x = Xc + r * cos(teta)
y = Yc + r * sen(teta)
```

Isso reproduz as proporções e os ângulos da imagem do enunciado:

- **12 vértices** — 6 internos + 6 externos;
- **30 arestas** — 6 do hexágono externo, 6 do hexagrama interno (dois triângulos),
  6 radiais e 12 diagonais;
- **20 faces**, das quais **10 são visíveis**, e essas 10 faces triangulares
  recobrem exatamente a área do hexágono (verificado: a soma das áreas dos 10
  triângulos é igual a `3√3/2 · R²`, a área do hexágono).

Numeração das faces: **1** é a central; **2, 3, 4** são as adjacentes a ela;
**5 a 10** são as faces dos cantos.
