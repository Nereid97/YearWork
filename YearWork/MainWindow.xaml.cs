using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YearWork {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        enum Grids { ChosingCategory, ChosingPerson, LabelGrid }
        Grids CurrentGrid = Grids.LabelGrid;

        private void button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private void expander_Collapsed(object sender, RoutedEventArgs e) {
            listBox.SelectedIndex = -1;
        }
        private void SwitchGrid(Grids inGrid, Grids outGrid) {
            switch (inGrid) {
                case Grids.ChosingCategory:
                    ChoiseCategoryGridIn();
                    CurrentGrid = Grids.ChosingCategory;
                    break;
                case Grids.ChosingPerson:
                    CurrentGrid = Grids.ChosingPerson;
                    ChoisePersonGridIn();
                    break;
                case Grids.LabelGrid:
                    CurrentGrid = Grids.LabelGrid;
                    LabelGridIn();
                    break;
                default:
                    break;
            }
            switch (outGrid) {
                case Grids.ChosingCategory:
                    ChoiseCategoryGridOut();
                    break;
                case Grids.ChosingPerson:
                    ChoisePersonGridOut();
                    break;
                case Grids.LabelGrid:
                    LabelGridOut();
                    break;
                default:
                    break;
            }
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (listBox.SelectedIndex != -1) {
                lblChairName.Content = ((ListBoxItem)listBox.SelectedItem)?.Content.ToString();
                if(CurrentGrid != Grids.ChosingCategory)
                SwitchGrid(Grids.ChosingCategory, CurrentGrid);
            }
            else {
                if (listBox.SelectedIndex == -1 && CurrentGrid == Grids.ChosingCategory) {
                    SwitchGrid(Grids.LabelGrid, Grids.ChosingCategory);
                }
            }
        }
        private void ChoiseCategoryGridIn() {
            CurrentGrid = Grids.ChosingCategory;
            TranslateTransform trHead = new TranslateTransform(0, 234);
            TranslateTransform trTeachers = new TranslateTransform(0, 234);
            TranslateTransform trStudents = new TranslateTransform(0, 234);
            TranslateTransform trLabel = new TranslateTransform(0, -90);
            lblChairName.RenderTransform = trLabel;
            btnHead.RenderTransform = trHead;
            btnStudents.RenderTransform = trStudents;
            btnTeachers.RenderTransform = trTeachers;
            DoubleAnimation daLabel = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200), EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut } };
            DoubleAnimation daButton = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200), EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut } };

            daLabel.To = 0;
            daLabel.From = -90;


            daButton.From = 234;
            daButton.To = 0;

            trLabel.BeginAnimation(TranslateTransform.YProperty, daLabel);
            trHead.BeginAnimation(TranslateTransform.YProperty, daButton);

            daButton.BeginTime = TimeSpan.FromMilliseconds(100);

            trTeachers.BeginAnimation(TranslateTransform.YProperty, daButton);

            daButton.BeginTime = TimeSpan.FromMilliseconds(200);

            trStudents.BeginAnimation(TranslateTransform.YProperty, daButton);
        }
        private void ChoiseCategoryGridOut() {
            TranslateTransform trHead = new TranslateTransform(0, 0);
            TranslateTransform trTeachers = new TranslateTransform(0, 0);
            TranslateTransform trStudents = new TranslateTransform(0, 0);
            TranslateTransform trLabel = new TranslateTransform(0, 0);
            lblChairName.RenderTransform = trLabel;
            btnHead.RenderTransform = trHead;
            btnStudents.RenderTransform = trStudents;
            btnTeachers.RenderTransform = trTeachers;
            DoubleAnimation daButton = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200), EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseIn } };
            DoubleAnimation daLabel = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200), EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseIn } };

            daLabel.To = -90;
            daLabel.From = 0;

            daButton.From = 0;
            daButton.To = 234;

            trLabel.BeginAnimation(TranslateTransform.YProperty, daLabel);
            trStudents.BeginAnimation(TranslateTransform.YProperty, daButton);

            daButton.BeginTime = TimeSpan.FromMilliseconds(100);

            trTeachers.BeginAnimation(TranslateTransform.YProperty, daButton);

            daButton.BeginTime = TimeSpan.FromMilliseconds(200);

            trHead.BeginAnimation(TranslateTransform.YProperty, daButton);
        }
        private void LabelGridIn() {
            CurrentGrid = Grids.LabelGrid;
            lblIsFacultyChosen.Opacity = 0;
            DoubleAnimation da = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200), BeginTime = TimeSpan.FromMilliseconds(200) };
            da.To = 1;
            lblIsFacultyChosen.BeginAnimation(Label.OpacityProperty, da);
        }
        private void LabelGridOut() {
            DoubleAnimation da = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(200) };
            da.To = 0;
            lblIsFacultyChosen.BeginAnimation(Label.OpacityProperty, da);
        }
        private void ChoisePersonGridIn() {
            listPersons.Visibility = Visibility.Visible;
            DoubleAnimation daOpacity = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            daOpacity.BeginTime = TimeSpan.FromMilliseconds(200);
            listPersons.BeginAnimation(ListBox.OpacityProperty, daOpacity);
        }
        private void ChoisePersonGridOut() {
            DoubleAnimation daOpacity = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(400));
            daOpacity.Completed += DaOpacity_Completed;
            listPersons.BeginAnimation(ListBox.OpacityProperty, daOpacity);
        }
        private void DaOpacity_Completed(object sender, EventArgs e) {
            listPersons.Visibility = Visibility.Hidden;
        }
        private void btnTeachers_Click(object sender, RoutedEventArgs e) {
            SwitchGrid(Grids.ChosingPerson, Grids.ChosingCategory);
        }
    }
}
