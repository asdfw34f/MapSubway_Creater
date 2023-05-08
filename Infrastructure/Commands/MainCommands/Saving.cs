using EditorSubwayMap.Data;
using System.Windows.Input;
using EditorSubwayMap.MVVM.Model.Rout;
using System.Windows.Documents;
using System.Collections.Generic;

namespace EditorSubwayMap.Infrastructure.Commands.MainCommands
{
    public class Saving
    {/*
        List<LineWay> Lines = new List<LineWay>();
        List<CircleWay> Circles = new List<CircleWay>();
        List<Station> Stations = new List<Station>();

        public ICommand SaveStation { get; }
        private bool CanSaveStation(object p) => true;
        private void OnSaveStation(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Station)
            {

            }
        }

        public ICommand SaveWay { get; }
        private bool CanSaveWay(object p) => true;
        private void OnSaveWay(object p)
        {
            if (OnCanvas.Drawing == OnCanvas.Modes.Line)
            {
                Lines.Add(
                    new LineWay()
                    {
                        Color = _ColorConvert.ConvertToString(Color),
                        endPoint = new System.Windows.Point(
                            OnCanvas.Line.X2, OnCanvas.Line.Y2),
                        NameWay = p.ToString(),
                        startPoint = new System.Windows.Point(
                            OnCanvas.Line.X1, OnCanvas.Line.Y1),
                        stations = null
                    });
                
                WayList.Add(.Text);

                lineWays.Add(
                    new LineWay()
                    {
                        WayID = AtrSt_NameWay.Text.ToString() + "ID",
                        NameWay = AWay_Name.Text,
                        Start = new Point(line.X1, line.Y1),
                        End = new Point(line.X2, line.Y2),
                        Color = cboColors.SelectedValue.ToString()
                    });
                line.ToolTip = "Ветка метро: " + AWay_Name.Text;
                line.Name = AtrSt_NameWay.Text.ToString();
                AWay_Name.Text = "Ветка добавлена";
                lWays.Add(line);
            }
            else if (OnCanvas.Drawing == OnCanvas.Modes.Circle)
            {
            }
        }
        public Saving()
        {
            SaveStation = new LambdaCommand(OnSaveStation, CanSaveStation);
        }*/
    }
}
