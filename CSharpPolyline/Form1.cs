using ExcelDataReader;
using System.Linq;

namespace CSharpPolyline
{
    public partial class Form1 : Form
    {
        private Dictionary<double, double> line_dict = new Dictionary<double, double>();
        private List<Polyline> polylines = new List<Polyline>();

        public Form1()
        {
            InitializeComponent();
        }

        private void BrowseFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string file_path = ofd.FileName.ToString();
            ExcelReader(file_path);
            GeneratePolylines();
            DisplayPolylines();
        }

        private void ExcelReader(string file_path)
        {
            Console.WriteLine("Reading Excel File");
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(file_path, FileMode.Open, FileAccess.Read)) 
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool first_row = true;
                    while (reader.Read())
                    {
                        if (!first_row)
                        {
                            this.line_dict.Add(reader.GetDouble(1), reader.GetDouble(2));
                        }
                        first_row = false;
                    }
                }
            }
        }

        private void GeneratePolylines()
        {
            List<double> first_polyline_nodes = new List<double>();
            foreach(var kvp in line_dict)
            {
                if (line_dict.ContainsValue(kvp.Key))
                {
                    continue;
                }
                else
                {
                    first_polyline_nodes.Add(kvp.Key);
                }
            }
            foreach(double node in first_polyline_nodes)
            {
                double curr_node = node;
                Polyline new_polyline = new Polyline();
                while(this.line_dict.ContainsKey(curr_node))
                {
                    Line new_line = new Line(curr_node, this.line_dict[curr_node]);
                    new_polyline.AddLine(new_line);
                    curr_node = this.line_dict[curr_node];
                }
                this.polylines.Add(new_polyline);
            }
        }

        private void DisplayPolylines()
        {
            int pl_count = this.polylines.Count;
            int max_line_count = 0;
            int pl_num = 0;
            foreach(Polyline pl in this.polylines)
            {
                max_line_count = Math.Max(max_line_count, pl.LineSegments.Count);
                Console.WriteLine(pl_num);
                foreach(Line ln in pl.LineSegments)
                {
                    Console.WriteLine(ln.StartNode.ToString() + " " + ln.EndNode.ToString());
                }
                pl_num++;
            }

            this.dataGridView1.ColumnCount = pl_count;
            this.dataGridView1.RowCount = max_line_count;

            for (int i = 0; i < this.polylines.Count; i++)
            {
                for (int j = 0; j < this.polylines[i].LineSegments.Count; j++)
                {
                    this.dataGridView1[i, max_line_count - 1 - j].Value = "|";
                }
            }
        }
    }
}
