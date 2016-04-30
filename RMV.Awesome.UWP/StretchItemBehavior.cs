using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RMV.Awesome.UWP
{
    public static class StretchItemBehavior
    {
        /// <summary>
        /// Declare new attached property. This property specifies the minium width
        /// for child items in an items wrap grid panel. Setting this property
        /// to an non zero value will enable dynamic sizing of items so that
        /// when items are wrapped the items control is always filled out horizontally
        /// i.e. the width of items are increased to fill the empty space.
        /// </summary>
        public static readonly DependencyProperty MinItemWidthProperty =
            DependencyProperty.RegisterAttached("MinItemWidth", typeof(double),
            typeof(StretchItemBehavior), new PropertyMetadata(0, OnMinItemWidthChanged));

                /// <summary>
                /// Only applicable when MinItemWidth is non zero. Typically the logic
                /// behind MinItemWidth will only trigger if the number of items is
                /// more than or equal to what a single row will accomodate. This property
                /// specifies that the layout logic is also performed when there are
                /// less items than what a single row will accomodate.
                /// </summary>
        public static readonly DependencyProperty FillBeforeWrapProperty =
          DependencyProperty.RegisterAttached("FillBeforeWrap", typeof(bool),
          typeof(StretchItemBehavior), new PropertyMetadata(false));

        /// <summary>
        /// This method will be called when the NavigateTo
        /// property is changed
        /// </summary>
        /// <param name="s">The sender (the Frame)</param>
        /// <param name="e">Some additional information</param>
        public static void OnMinItemWidthChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            //If the host object is a frame.
            if (s is ListViewBase)
            {
                //Unbox.
                ListViewBase f = s as ListViewBase;


                //Remove previous handler (if any.)
                f.SizeChanged -= listView_SizeChanged;


                //If property is a positive value.
                if (((double)e.NewValue) > 0)
                {
                    //Attach handling.
                    f.SizeChanged += listView_SizeChanged;
                }
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void listView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Unbox the sender.
            var itemsControl = sender as ListViewBase;


            //Retrieve items panel.
            ItemsWrapGrid itemsPanel = itemsControl.ItemsPanelRoot as ItemsWrapGrid;


            //If the items panel is a wrap grid.
            if (itemsPanel != null)
            {
                //Get total size (leave room for scrolling.)
                var total = e.NewSize.Width - 10;


                //Minimum item size.
                var itemMinSize = (double)itemsControl.GetValue(MinItemWidthProperty);


                //How many items can be fit whole.
                var canBeFit = Math.Floor(total / itemMinSize);


                //logic that if the total items
                //are less then the number of items that
                //would fit then devide the total size by
                //the number of items rather than the number
                //of items that would actually fit.
                if ((bool)itemsControl.GetValue(FillBeforeWrapProperty) &&
                    itemsControl.Items.Count > 0 &&
                    itemsControl.Items.Count < canBeFit)
                {
                    canBeFit = itemsControl.Items.Count;
                }


                //Set the items Panel item width appropriately.
                //Note you will need your container to stretch
                //along with the items panel or it will look
                //strange.
                //  <GridView.ItemContainerStyle>
                //< Style TargetType = "GridViewItem" >
                //< Setter Property = "HorizontalContentAlignment" Value = "Stretch" />
                // < Setter Property = "HorizontalAlignment" Value = "Stretch" />
                //</ Style >
                // </ GridView.ItemContainerStyle >
                itemsPanel.ItemWidth = total / canBeFit;
            }
        }


    }
}
