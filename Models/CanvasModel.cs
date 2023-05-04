﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EditorSubwayMap.Models
{
    public class CanvasModel : Base.Model
    {
        #region Fields
        private UIElement _Children = new UIElement();
        private Point _StartPoint = new Point();
        private Point _EndPoint = new Point();
        private Ellipse _Ellipse = new Ellipse();
        private Line _Line = new Line();
        private bool _IsDrawing;
        #endregion

        public Line Line
        {
            get => _Line; 
            set => Set(ref _Line, value);
        }

        public Ellipse Ellipse
        {
            get => _Ellipse; 
            set => Set(ref _Ellipse, value);
        }
       
    //    public UIElement Child
    //    {
    //        get => _Children;
    //        set
    //        {
    //            if (!Set(ref _Children, value))
    //                this.Canvas.Children.Add(value);
    //        }
    //    }
        
        private Canvas _canvas = new Canvas() { Background=Brushes.Black};
        public Canvas Canvas
        {
            get => _canvas;
            set => Set(ref _canvas, value);
        }

        public bool IsDrawing
        {
            get => _IsDrawing;
            set=> Set(ref _IsDrawing, value);
        }

        public Point StartPoint
        {
            get => _StartPoint;
            set => Set(ref _StartPoint, value);
        }

        public Point EndPoint
        {
            get => _EndPoint; 
            set => Set(ref _EndPoint, value);
        }
    }
}