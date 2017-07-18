using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace boss.client.win.common.controls
{
    public class FormPanel : Panel
    {
        public static readonly DependencyProperty GroupSpaceProperty = DependencyProperty.Register("GroupSpace", typeof(double), typeof(FormPanel), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty LineSpaceProperty = DependencyProperty.Register("LineSpace", typeof(double), typeof(FormPanel), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty ColumnSpaceProperty = DependencyProperty.Register("ColumnSpace", typeof(double), typeof(FormPanel), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty MinContentWidthProperty = DependencyProperty.Register("MinContentWidth", typeof(double), typeof(FormPanel), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty StretchContentProperty = DependencyProperty.Register("StretchContent", typeof(bool), typeof(FormPanel), new PropertyMetadata(default(bool)));
        private const int GroupColumns = 2;
        private int numberOfColumnsInLine;
        private double[] columnsWidth = new double[0];
        private double[] linesHeigth = new double[0];

        public FormPanel()
        {
            ClipToBounds = true;
            Loaded += FormPanel_OnLoaded;
        }

        public double MinContentWidth
        {
            get { return (double)GetValue(MinContentWidthProperty); }
            set { SetValue(MinContentWidthProperty, value); }
        }

        public double LineSpace
        {
            get { return (double)GetValue(LineSpaceProperty); }
            set { SetValue(LineSpaceProperty, value); }
        }

        public double GroupSpace
        {
            get { return (double)GetValue(GroupSpaceProperty); }
            set { SetValue(GroupSpaceProperty, value); }
        }

        public double ColumnSpace
        {
            get { return (double)GetValue(ColumnSpaceProperty); }
            set { SetValue(ColumnSpaceProperty, value); }
        }

        public bool StretchContent
        {
            get { return (bool)GetValue(StretchContentProperty); }
            set { SetValue(StretchContentProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            for (var i = 0; i < Children.Count; ++i)
            {
                if (i % GroupColumns == 1) continue;
                Children[i].Measure(constraint);
            }

            var adjustmentSwitch = StretchContent ? 1.0 : 0.0;
            for (var candidate = GetMaxNumberOfGroups(constraint); candidate > 0; --candidate)
            {
                var candidateWidthList = CalculateColumnsFinalWidth(candidate);
                var totalWidth = CalculateTotalWidth(candidateWidthList);
                if (!(totalWidth < constraint.Width)) continue;
                numberOfColumnsInLine = candidate * GroupColumns;
                columnsWidth = candidateWidthList;
                var adjustment = (constraint.Width - totalWidth) / candidate * adjustmentSwitch;
                for (var i = 0; i < candidate; ++i)
                {
                    columnsWidth[i * GroupColumns + 1] += adjustment;
                }

                for (var i = 0; i < Children.Count; ++i)
                {
                    if (i % GroupColumns != 1) continue;
                    var column = i % numberOfColumnsInLine;
                    var frameworkElement = Children[i] as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        Children[i].Measure(new Size(columnsWidth[column], constraint.Height));
                        frameworkElement.Width = columnsWidth[column];
                    }
                    else
                    {
                        Children[i].Measure(new Size(columnsWidth[column], constraint.Height));
                    }
                }
                break;
            }

            var numnberOfLines = numberOfColumnsInLine == 0 ? 0: Children.Count / numberOfColumnsInLine + (Children.Count % numberOfColumnsInLine != 0 ? 1 : 0);
            linesHeigth = new double[numnberOfLines];
            for (var i = 0; i < numnberOfLines; ++i)
            {
                linesHeigth[i] = GetChildrenOfLine(i, numberOfColumnsInLine).Select(x => x.DesiredSize.Height).Max();
            }

            return new Size(constraint.Width, linesHeigth.Sum() + Math.Max(0, numnberOfLines - 1) * LineSpace);
        }

        private double CalculateTotalWidth(double[] widthList)
        {
            var numberOfGroups = widthList.Length / GroupColumns;
            return widthList.Sum() + (numberOfGroups - 1) * GroupSpace + numberOfGroups * (GroupColumns - 1) * ColumnSpace;
        }

        private double[] CalculateColumnsFinalWidth(int numberOfGroupsInLine)
        {
            var result = new double[numberOfGroupsInLine * GroupColumns];
            for (var i = 0; i < numberOfGroupsInLine; ++i)
            {
                result[i * GroupColumns] = GetChildrenOfColumn(i * GroupColumns, numberOfGroupsInLine).Select(x => x.DesiredSize.Width).Max();
                result[i * GroupColumns + 1] = MinContentWidth;
            }
            return result;
        }

        private IEnumerable<UIElement> GetChildrenOfColumn(int column, int numberOfGroupsInLine)
        {
            var lineColumns = numberOfGroupsInLine * GroupColumns;
            return GetChildren().Select((item, index) => new KeyValuePair<int, UIElement>(index, item)).Where(x => x.Key % lineColumns == column).Select(x => x.Value);
        }

        private IEnumerable<UIElement> GetChildrenOfLine(int line, int numberOfColumnsInLine)
        {
            return GetChildren().Select((item, index) => new KeyValuePair<int, UIElement>(index, item)).Where(x => x.Key / numberOfColumnsInLine == line).Select(x => x.Value);
        }

        private IEnumerable<UIElement> GetChildren()
        {
            for (var i = 0; i < Children.Count; ++i)
            {
                yield return Children[i];
            }
        }

        private double GetGroupDesiredWidth(int groupIndex)
        {
            return ColumnSpace + Children[groupIndex * GroupColumns].DesiredSize.Width + MinContentWidth;
        }

        private int GetMaxNumberOfGroups(Size constraint)
        {
            double maxWidth = 0;
            var numberOfGroups = Children.Count / GroupColumns + (Children.Count % GroupColumns != 0 ? 1 : 0);
            for (var i = 0; i < numberOfGroups; ++i)
            {
                maxWidth += GetGroupDesiredWidth(i) + (i == 0 ? 0 : GroupSpace);
                if (maxWidth > constraint.Width)
                {
                    return i;
                }
            }
            return numberOfGroups;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            for (var i = 0; i < Children.Count / GroupColumns + (Children.Count % GroupColumns == 0 ? 0 : 1); ++i)
            {
                ArrangeGroup(i);
            }
            return arrangeSize;
        }

        private void ArrangeGroup(int index)
        {
            var startColumn = index * GroupColumns % numberOfColumnsInLine;
            var groupColumn = startColumn / GroupColumns;
            var line = index * GroupColumns / numberOfColumnsInLine;
            var x = columnsWidth.Take(startColumn).Sum() + groupColumn * GroupSpace + (groupColumn + 1) * (GroupColumns - 1) * ColumnSpace;
            var y = linesHeigth.Take(line).Sum() + line * LineSpace;
            var offset = 0.0;
            for (var i = 0; i < GroupColumns; ++i)
            {
                if (i + index * GroupColumns >= Children.Count) break;
                var rect = new Rect(new Point(x + offset, y), new Size(columnsWidth[startColumn + i], linesHeigth[line]));
                Children[index * GroupColumns + i].Arrange(rect);
                offset += columnsWidth[startColumn + i] + ColumnSpace;
            }
        }

        private void FormPanel_OnLoaded(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, this);
            MoveElementFocus(FocusNavigationDirection.First);
        }

        protected static void MoveElementFocus(FocusNavigationDirection focusNavigationDirection)
        {
            var element = Keyboard.FocusedElement as UIElement;
            element?.MoveFocus(new TraversalRequest(focusNavigationDirection));
        }
    }
}
