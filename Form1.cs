namespace WinFormsApp1
{


    // 
    //  Секция 1: описание основных переменных, классов и конструктор формы.
    // 
    public partial class Form1 : Form
    {
        public const int MaximumAmount = 50;                // Максимальное количество парабол
        public const int Precision = 15;                    // Количество вычисляемых точек параболы минус 1
        public const float MaximumValueOfParameter = 100;   // Минимальное значение параметра
        public const float MinimumValueOfParameter = -100;  // Максимальное значение параметра

        public static List<parabola> parabolas = new List<parabola>();                              // Список с параболами
        public static List<TextBox[]> txtBoxes = new List<TextBox[]>(0);                            // Списко с textBox-ами
        public static int pressing_number = 1;                                                      // Переменная дла обработки графического ввода
        public static PointF point = new PointF(float.NegativeInfinity, float.NegativeInfinity);    // Точка, которая используется для графического ввода

        //  Класс, который описывает параболы
        public class parabola
        {
            public float a, b, c;
            public parabola()
            {
                a = 1; b = 0; c = 0; // Параметр а обязательно не равен 0
            }
            public parabola(float a, float b, float c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

        };
        //  Класс, в короом находится основная информация о 2 параболах
        public class parabolasInfo
        {
            public float x1, x2,                // Точки пересечения 2 парабол (если они есть)
                         surfaceArea;           // Площадь фигуры, ораниченной двумя параболами (если она есть)
            public parabola parabol1, parabol2; // Непосредственно сами параболы
            
            public parabolasInfo(parabola p1, parabola p2) // Конструктор
            {
                parabol1 = p1;
                parabol2 = p2;

                float D1_2 = (p1.b - p2.b) * (p1.b - p2.b) - 4 * (p1.a - p2.a) * (p1.c - p2.c); // Дискриминант разницы 2 уравнений параболы

                if (D1_2 > 0) // Параболы пересекаются?
                {
                    x1 = (float)(-(p1.b - p2.b) + Math.Sqrt(D1_2)) / (2 * (p1.a - p2.a));
                    x2 = (float)(-(p1.b - p2.b) - Math.Sqrt(D1_2)) / (2 * (p1.a - p2.a));
                    if (x1 > x2)
                    {
                        float temp = x1;
                        x1 = x2;
                        x2 = temp;
                    }
                    float factor1 = ((p1.a - p2.a) * (x1 * x1 * x1 - x2 * x2 * x2)) / 3,    // По формуле считаем площадь
                          factor2 = ((p1.b - p2.b) * (x1 * x1 - x2 * x2)) / 2,
                          factor3 = ((p1.c - p2.c) * (x1 - x2));
                    surfaceArea = Math.Abs(factor1 + factor2 + factor3);
                }
                else
                {
                    x1 = 0; x2 = 0;
                    surfaceArea = float.NaN; // Если не пересекаются, то площадь делаем не числом.
                }
            }
            public parabolasInfo() // Конструктор по умолчанию
            {
                surfaceArea = float.NaN;
                parabol1 = null;
                parabol2 = null;
                x1 = 0; x2 = 0;
            }
        }

        //  Сама форма
        public Form1()
        {
            
            InitializeComponent();
            pictureBox1.Image = DrawParabolas();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //   Секция 2:
        //   Математическая и графическая части проекта: рисовка парабол и координатной сетки, нахождение 2 парабол,
        //   образующих наибольшую площадь.

        // Рисование внутреннего пространства фигуры, ограниченной двумя параболами
        void DrawShape(parabolasInfo pInfo, int scale, Graphics graphCanvas) 
        {
            Pen pen = new Pen(Color.DarkCyan, 7.5f); //  Здесь мы число находим большую и меньшую точки пересечения парабол
            
            float minX = (pictureBox1.Width / 2 + pInfo.x1 * trackBar1.Value > 0) ? (pInfo.x1 * trackBar1.Value / 7.5f) : -pictureBox1.Width / 2 / 7.5f,                        
                  maxX = (pictureBox1.Width / 2 + pInfo.x2 * trackBar1.Value < pictureBox1.Width) ? (pInfo.x2 * trackBar1.Value / 7.5f) : (pictureBox1.Width / 2 / 7.5f);       
            for(float x = minX; x <= maxX; x++)                                                                                                             
            {
                float temp = x / trackBar1.Value * 7.5f;                                                                                                        
                float y1 = pictureBox1.Height / 2 - trackBar1.Value * ((pInfo.parabol1.a * temp * temp) + (pInfo.parabol1.b * temp) + (pInfo.parabol1.c));
                float y2 = pictureBox1.Height / 2 - trackBar1.Value * ((pInfo.parabol2.a * temp * temp) + (pInfo.parabol2.b * temp) + (pInfo.parabol2.c));
                graphCanvas.DrawLine(pen, (float)pictureBox1.Width / 2 + x * 7.5f, y1, (float)pictureBox1.Width / 2 + x * 7.5f, y2);                        // Рисуем внутреннюю область фигуры.
            }
        }

        void DrawFigure(parabolasInfo pInfo, Graphics graphCanvas)
        {
            Pen pen = new Pen(Color.Blue, 7);
            float deltaX = Math.Abs(pInfo.x2 - pInfo.x1) / 10;
            PointF[] points1 = new PointF[11];
            PointF[] points2 = new PointF[11];
            int j = 0;
            for (float i = pInfo.x1; i <= pInfo.x2+0.1f; i+=deltaX)
            {
                if (j < points1.Length)
                {
                    points1[j] = new PointF(pictureBox1.Width / 2 + i * trackBar1.Value, pictureBox1.Height / 2 - (pInfo.parabol1.a * i * i + pInfo.parabol1.b * i + pInfo.parabol1.c) * trackBar1.Value);
                    points2[j] = new PointF(pictureBox1.Width / 2 + i * trackBar1.Value, pictureBox1.Height / 2 - (pInfo.parabol2.a * i * i + pInfo.parabol2.b * i + pInfo.parabol2.c) * trackBar1.Value);
                }
                else
                    break;
                j++;
            }
            graphCanvas.DrawCurve(pen, points1);
            graphCanvas.DrawCurve(pen, points2);
        }
        // Рисовка координатной сетки
        void DrawAxis(Graphics graphCanvas, int scale) 
        {
            Pen pen = new Pen(Color.White, 0.05f);       // Ручка для рисовки тонких линий
            Pen penForAxis = new Pen(Color.White, 3f);   // Ручка для рисования осей координат
            graphCanvas.DrawLine(penForAxis, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2); // Рисуем ось абсцисс
            float delta = 6;
            for(int i = 0; i < pictureBox1.Width / 2 / scale + 1; i++) // Цикл для рисования делений и тонких линий
            {
                graphCanvas.DrawLine(penForAxis, pictureBox1.Width / 2 + i * scale, pictureBox1.Height / 2 - delta, pictureBox1.Width / 2 + i * scale, pictureBox1.Height / 2 + delta);    // Рисуем деления
                graphCanvas.DrawLine(penForAxis, pictureBox1.Width / 2 - i * scale, pictureBox1.Height / 2 - delta, pictureBox1.Width / 2 - i * scale, pictureBox1.Height / 2 + delta);
                graphCanvas.DrawLine(pen, pictureBox1.Width / 2 + i * scale, 0, pictureBox1.Width / 2 + i * scale, pictureBox1.Height);  // Рисуем тонкие линии
                graphCanvas.DrawLine(pen, pictureBox1.Width / 2 - i * scale, 0, pictureBox1.Width / 2 - i * scale, pictureBox1.Height);             
            }
            graphCanvas.DrawLine(penForAxis, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);  // Рисуем ось ординат
            for (int i = 0; i < pictureBox1.Height / 2 / scale + 1; i++) // Все то же самое, но теперь по оси y
            {
                graphCanvas.DrawLine(penForAxis, pictureBox1.Width / 2 - delta - 1, pictureBox1.Height / 2 + i * scale, pictureBox1.Width / 2 + delta+1, pictureBox1.Height / 2 + i * scale);
                graphCanvas.DrawLine(penForAxis, pictureBox1.Width / 2 - delta - 1, pictureBox1.Height / 2 - i * scale, pictureBox1.Width / 2 + delta+1, pictureBox1.Height / 2 - i * scale);
                graphCanvas.DrawLine(pen, 0, pictureBox1.Height / 2 + i * scale, pictureBox1.Width, pictureBox1.Height / 2 + i * scale);
                graphCanvas.DrawLine(pen, 0, pictureBox1.Height / 2 - i * scale, pictureBox1.Width, pictureBox1.Height / 2 - i * scale);
            }

        }
        

        // Функция, возвращающая массив точек параболы.
        PointF[] paraboloid(parabola parabol)
        {
            float min = (float)-pictureBox1.Width / 2 / trackBar1.Value;
            float max = -min;
            float deltaX = (max - min) / Precision;
            PointF[] parabola = new PointF[Precision+1];
            int j = 0;
            for (float i = min; i <= max+0.1f; i += deltaX)
            {
                if (j != Precision+1)
                    parabola[j++] = new PointF(pictureBox1.Width / 2 + i * trackBar1.Value, pictureBox1.Height / 2 - (parabol.a * i * i + parabol.b * i + parabol.c) * trackBar1.Value);
                else
                    break;
            }
            return parabola;
        }

        // Функция для нахождения нужной пары парабол.
        parabolasInfo FindTheParabolas(List<parabola> p)    
        {
            if (p.Count < 2) return null;                           // Очевидно, что если парабола одна или её вовсе нет, то искать ничего не нужно
            if (p.Count < 3) return new parabolasInfo(p[0], p[1]);
            parabolasInfo answer = new parabolasInfo(p[0], p[1]);// Если парабол две, то возвращаем эту пару
            for (int i = 0; i < p.Count-1; i++)  // Цикл для нахождения нужной пары парабол
            {
                parabolasInfo temp = new parabolasInfo();
                for (int j = i + 1; j < p.Count; j++)                   
                    temp = new parabolasInfo(p[i], p[j]);
                if ((answer.surfaceArea < temp.surfaceArea // Сравниваем площади фигур внутри парабол.
                    && !float.IsNaN(answer.surfaceArea) 
                    && !float.IsNaN(temp.surfaceArea)) 
                    || (float.IsNaN(answer.surfaceArea) 
                    && !float.IsNaN(temp.surfaceArea)))
                    answer = temp;
            }
            return answer;
        }

        // Рисовка парабол
        Bitmap DrawParabolas() 
        {
            Pen pen = new Pen(Color.White, 3);
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphCanvas = Graphics.FromImage(canvas);
            float temp;
            for(int i = 0; i < parabolas.Count; i++)    // Присваиваем значению коэффициента каждой параболы соответствующее значение из TextBox-а
            {
                parabolas[i].a = float.TryParse(txtBoxes[i][0].Text, out temp) && temp != 0 && Math.Abs(temp) <= MaximumValueOfParameter ? temp : parabolas[i].a;
                parabolas[i].b = float.TryParse(txtBoxes[i][1].Text, out temp) && Math.Abs(temp) <= MaximumValueOfParameter ? temp : parabolas[i].b;
                parabolas[i].c = float.TryParse(txtBoxes[i][2].Text, out temp) && Math.Abs(temp) <= MaximumValueOfParameter ? temp : parabolas[i].c;
            }

            for (int i = 0; i < parabolas.Count; i++)   // В данном цикле мы изменяем цвет текстов TextBox-ов на белый, если они белыми не были.
            {
                for(int j = 0; j < 3; j++)
                {
                    if(txtBoxes[i][j].ForeColor == Color.Cyan)
                        txtBoxes[i][j].ForeColor = Color.White;
                }
            }
            parabolasInfo answer = FindTheParabolas(parabolas); // Ищем ответ
            if (answer != null && answer.surfaceArea > 0 && !float.IsNaN(answer.surfaceArea))   // Ответ есть?
            {
                DrawShape(answer, trackBar1.Value, graphCanvas);    // Тогда рисуем фигуру, ограниченную двумя параболами
            }
            DrawAxis(graphCanvas, trackBar1.Value); // Рисуем оси координат
            for (int i = 0; i < parabolas.Count; i++) // Рассматриваем параболы
            {
                if (answer != null)
                    if ((parabolas[i] == answer.parabol1 || parabolas[i] == answer.parabol2) && answer.surfaceArea > 0 && !float.IsNaN(answer.surfaceArea))
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            txtBoxes[i][j].ForeColor = Color.Cyan;              // Если парабола из списка parabolas образует фигуру с наибольшей площадью,  
                        }                                                       // то тексты соответствующей ей строки TextBox-ов красим в голубой.
                        txtBoxes[i][0].Text = Convert.ToString(parabolas[i].a); // Если до этого был некорректный ввод, возвращаем его к тому значению, 
                        txtBoxes[i][1].Text = Convert.ToString(parabolas[i].b); // который был до внесения изменений.
                        txtBoxes[i][2].Text = Convert.ToString(parabolas[i].c);
                        continue;
                    }
                
            }
            for(int i = 0; i < parabolas.Count; i++) // Рисуем параболы
            {
                graphCanvas.DrawCurve(pen, paraboloid(parabolas[i]));
            }
            if (answer != null && answer.surfaceArea > 0 && !float.IsNaN(answer.surfaceArea)) // Если нужные параболы есть, то красим их в отдельный цвет
            {
                Pen tempPen = new Pen(Color.Cyan, 4);
                graphCanvas.DrawCurve(tempPen, paraboloid(answer.parabol1));
                graphCanvas.DrawCurve(tempPen, paraboloid(answer.parabol2));
                DrawFigure(answer, graphCanvas);
            }
            if (pressing_number == 2 && point != new PointF(float.NegativeInfinity, float.NegativeInfinity)) // Не забываем про то, что пользователь мог не сделать второго нажатия для рисовки параболы
            {
                graphCanvas.DrawEllipse(new Pen(Color.Yellow, 5), ((float)pictureBox1.Width / 2 / trackBar1.Value + point.X) * trackBar1.Value - 3, ((float)pictureBox1.Height / 2 / trackBar1.Value - point.Y) * trackBar1.Value - 3, 4, 4);
            }
            return canvas;
        }

        //  Секция 3:
        //  Обработка событий формы и реализация ввода парабол с помощью
        //  нажатия на экран, а также создание дополнительных TextBox-ов при изменении
        //  количества парабол.

        //  Обработчик события прокручивания дорожки
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label7.Text = String.Format("{0}", trackBar1.Value);
            pictureBox1.Image = DrawParabolas();
        }

        //  Обработчик события изменения текста TextBox-а, который соответствует значеную либо коэффициента b, либо коэффициента c.
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            float temp = 0;
            if(!float.TryParse(tb.Text, out temp) || Math.Abs(temp) > MaximumValueOfParameter)
            {
                tb.ForeColor = Color.Red;
                return;
            }
            (sender as TextBox).Text = tb.Text;
            pictureBox1.Image = DrawParabolas();
        }
        //  Обработчик события изменения текста TextBox-а, который соответствует значению коэффицента a.
        //  Эти обработчики событий реализованы отдельно, так как коэффициенты b и c могут быть равными 0, в отличии от коэффициента а.
        private void textBox_TextChanged_a(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            float temp = 0;
            if (!float.TryParse(tb.Text, out temp) || temp == 0 || Math.Abs(temp) > MaximumValueOfParameter)
            {
                tb.ForeColor = Color.Red;
                return;
            }
            tb.ForeColor = Color.White;
            (sender as TextBox).Text = tb.Text;
            pictureBox1.Image = DrawParabolas();
        }
        // Обработчик события изменения текста TextBox-а, который регулирует количество парабол
        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            int Num;
            if (!int.TryParse(AmountTextBox.Text, out Num))
                AmountTextBox.Text = "";
            if (string.IsNullOrEmpty(AmountTextBox.Text))
                Num = 0;
            if (Num > MaximumAmount)
            {
                AmountTextBox.Text = "50";
                Num = MaximumAmount;
            }
            if (Num < 0)
            {
                AmountTextBox.Text = "0";
                Num = 0;
            }
            Panel_Resize(Num);  // Меняем количество парабол и TextBox-ов
            pictureBox1.Image = DrawParabolas();    // Выводим на экран новую картинку
        }
        //  Изменение количества парабол и TextBox-ов в соответствии со значением в textBox4.
        private void Panel_Resize(int num)
        {
            int temp = txtBoxes.Count;
            panel1.Controls.Clear();    // Удаляем с панели все TextBox-ы
            if (num > txtBoxes.Count)   // Добавляем новые параболы и строки TextBox-ов, если нам нужно получить больше парабол
                for(int i = txtBoxes.Count; i < num; i++)
                {
                    txtBoxes.Add(new TextBox[3]);
                    parabolas.Add(new parabola());
                    for (int j = 0; j < 3; j++)
                        txtBoxes[i][j] = new TextBox();
                }

            else
                for(int i = txtBoxes.Count-1; i >= num; i--) // А иначе уменьшаем количестов парабол и строк TextBox-ов
                {
                    txtBoxes.RemoveAt(i);
                    parabolas.RemoveAt(i);
                }
            for (int i = 0; i < num; i++) // Располагаем TextBox-ы так, как нам нужно.
            {
                for (int j = 0; j < 3; j++)
                {
                    txtBoxes[i][j].Size = new Size(35, 35);
                    txtBoxes[i][j].Location = new Point(10 + 40 * j, 10 + 40 * i);
                    if (txtBoxes[i][j].Text == "" && i >= temp)
                    {
                        int random = new Random().Next(50) - 25;
                        if (j == 0)
                        {
                            txtBoxes[i][j].Text = Convert.ToString(random != 0 ? random : 1);
                        }
                        else
                        txtBoxes[i][j].Text = Convert.ToString(random);
                        
                    }
                    if (j == 0)
                        txtBoxes[i][j].TextChanged += textBox_TextChanged_a;
                    else
                        txtBoxes[i][j].TextChanged += textBox_TextChanged;
                    txtBoxes[i][j].BackColor = Color.Black;
                    txtBoxes[i][j].ForeColor = Color.White;
                    panel1.Controls.Add(txtBoxes[i][j]);    // Добавляем ноый TextBox в панель
                }
            }
        }

        // Обработчик события прокрутки колёсиком мышки
        private void Form1_MouseWheel(object sender, MouseEventArgs e) 
        {
            if(trackBar1.Value + Math.Sign(e.Delta)*2 >= trackBar1.Minimum &&
               trackBar1.Value + Math.Sign(e.Delta)*2 <= trackBar1.Maximum) // Делаем проверку на допустимость прокрутки
                trackBar1.Value += Math.Sign(e.Delta) * 2;
            label7.Text = string.Format("{0}", trackBar1.Value);
            pictureBox1.Image = DrawParabolas();
        }
        //  Обработчик события нажатия на кнопку, отвечающую за чтение из файла парабол
        private void ReadFromFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Удалось ли открыть диалоговое окно с выбором файла?
            {
                string name = openFileDialog1.FileName;
                string[] input = File.ReadAllLines(name);
                int amount_Of_Inputs = input.Count();
                if(input.Count() > MaximumAmount)  // Количество парабол в строке больше допустимого?
                {
                    Form2 dlg = new Form2();
                    dlg.label1.Text = "An error occurred: too many parabolas!"; // Тогда говорим об этом пользователю
                    dlg.ShowDialog();
                    AmountTextBox.Text = "50";  // И выводим только 50 парабол
                    amount_Of_Inputs = MaximumAmount;
                }
                txtBoxes.Clear();
                parabolas.Clear();
                Panel_Resize(amount_Of_Inputs);
                for (int i = 0; i < amount_Of_Inputs; i++)
                {
                    string[] lineInput = input[i].Split(' ');
                    //Panel_Resize(i+1);  // Без этого не работает
                    for (int j = 0; j < 3; j++)
                    {
                        if (j < lineInput.Count())
                        {
                            if (!float.TryParse(lineInput[j], out float num))
                            {
                                Form2 dlg = new Form2();
                                dlg.ShowDialog();
                                panel1.Controls.Clear();
                                txtBoxes.Clear();
                                parabolas.Clear();
                                AmountTextBox.Text = "0";
                                return;
                            }
                            else
                            {
                                txtBoxes[i][j].Text = lineInput[j];
                            }
                        }
                        else
                            txtBoxes[i][j].Text = "0";
                        
                    }
                       
                }
                AmountTextBox.Text = Convert.ToString(amount_Of_Inputs);
            }
            pictureBox1.Image = DrawParabolas(); // Рисуем параболы
        }
        //  Обработка нажатия ЛКМ на график
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Нажали ЛКМ?
            {
                Point p = pictureBox1.PointToClient(Cursor.Position); // Получаем положение указателя мышки
                Bitmap tempImage = pictureBox1.Image as Bitmap; // Без этого не работает рисовка
                Graphics graph = Graphics.FromImage(tempImage);
                Pen pen = new Pen(Color.Yellow, 5);
                if (pressing_number == 1)   // Первое нажатие?
                {
                    point = new PointF((float)(p.X - pictureBox1.Width/2) / trackBar1.Value, (float)(pictureBox1.Height / 2 - p.Y) / trackBar1.Value); // Сохраняем координаты вершины
                    label1.Text = string.Format("{0:f2}", point.X);
                    label2.Text = string.Format("{0:f2}", point.Y);
                    pressing_number++;
                    graph.DrawEllipse(pen, p.X - 3, p.Y - 3, 4, 4); // Рисуем вершину параболы
                    pictureBox1.Image = tempImage;
                    return;
                }
                if (pressing_number == 2)   // Второе нажатие?
                {
                    if (parabolas.Count + 1 < MaximumAmount)   // Хватает ли места для параболы?
                    {
                        PointF tempPoint = new PointF((float)(p.X - pictureBox1.Width / 2) / trackBar1.Value, (float)(pictureBox1.Height / 2 - p.Y) / trackBar1.Value); // Запоминаем координаты второй точки
                        float a = (float)(point.Y - tempPoint.Y) / ((float)(point.X * point.X - tempPoint.X * tempPoint.X) - (float)(2 * point.X * (point.X - tempPoint.X)));   // И по формуле выводим сначала а
                        if(Math.Abs(a) > MaximumValueOfParameter || Math.Abs(a) < 0.0001f) // Параметр а имеет допустимое значение?
                        {
                            Form2 dlg = new Form2();
                            dlg.ShowDialog();
                            return;
                        }    
                        float b = -2 * a * (float)point.X;  // Затем считаем b
                        float c = point.Y - b * (float)point.X - a * (float)(point.X * point.X);    // И с (коэффициент)
                        parabola tempParabola = new parabola(a, b, c); // Создаем новую параболу
                        label3.Text = string.Format("{0:f2}", tempPoint.X);
                        label5.Text = string.Format("{0:f2}", tempPoint.Y);

                        Panel_Resize(parabolas.Count + 1);  // Создаем место для новой параболы
                        AmountTextBox.Text = Convert.ToString(parabolas.Count);
                        txtBoxes[parabolas.Count - 1][0].Text = Convert.ToString(tempParabola.a);   // И вписываем в TextBox-ы значения коэффициентов
                        txtBoxes[parabolas.Count - 1][1].Text = Convert.ToString(tempParabola.b);
                        txtBoxes[parabolas.Count - 1][2].Text = Convert.ToString(tempParabola.c);
;
                    }
                    else
                    {
                        Form2 dlg = new Form2(); // Если мест не хватает, то выводим ошибку и не рисуем параболу
                        dlg.ShowDialog();
                    }
                    pressing_number = 1;
                }
            }
        }

        private void EraseButton_Click(object sender, EventArgs e) // Обработчик события нажатия на кнопку, которая убирает все параболы
        {
            Panel_Resize(0);    // Просто меняем количество парабол на 0
            AmountTextBox.Text = "0";
            pictureBox1.Image = DrawParabolas();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) // Обработчик открытия диалогового окна с выбором файлов
        {
            string ext = Path.GetExtension(openFileDialog1.FileName);
            if (string.Compare(ext, ".url", true) == 0) e.Cancel = true;
        }


    }
}