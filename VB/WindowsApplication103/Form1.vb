Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports System.Reflection
Imports System.Collections

Namespace WindowsApplication103
	Partial Public Class Form1
		Inherits Form
		Implements IMessageFilter
		Public Sub New()
			InitializeComponent()
			Application.AddMessageFilter(Me)
			ToolTipList.Add(New ToolTipObject(barButtonItem1,"ToolTip","CToolTip"))
			ToolTipList.Add(New ToolTipObject(barButtonItem2,"AnotherToolTip","ToolTipShownInCustomizationMode"))
			ToolTipList.Add(New ToolTipObject(barButtonItem3, "Some","It's Work!"))

		End Sub

		Private ToolTipList As New List(Of ToolTipObject)()
		Private Const WM_MouseMove As Integer = &H0200


		Private Function GetLinksScreenRect(ByVal link As BarItemLink) As Rectangle
			Dim info As PropertyInfo = GetType(BarItemLink).GetProperty("BarControl", BindingFlags.Instance Or BindingFlags.NonPublic)
			Dim c As Control = CType(info.GetValue(link, Nothing), Control)
			Return c.RectangleToScreen(link.Bounds)
		End Function



		#Region "IMessageFilter Members"

		Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
				Dim BarItemRect As New Rectangle()
				Dim index As Integer = 0
				If m.Msg = WM_MouseMove Then
					For i As Integer = 0 To ToolTipList.Count - 1
						Try
							BarItemRect = GetLinksScreenRect(ToolTipList(i).ToolTipItem.Links(0))
						Catch
						End Try
						If (Not BarItemRect.IsEmpty) Then
							If BarItemRect.Contains(MousePosition) Then
								index = i
								Exit For
							End If
						End If

					Next i

					If BarItemRect.Contains(MousePosition) Then
						Dim te As New ToolTipControllerShowEventArgs()
						te.ToolTipLocation = ToolTipLocation.Fixed
						te.SuperTip = New SuperToolTip()
						If (Not barManager1.IsCustomizing) Then
							te.SuperTip.Items.Add(ToolTipList(index).ToolTip)
						Else
							te.SuperTip.Items.Add(ToolTipList(index).ToolTipCustomizationMode)
						End If
						Dim linkPoint As New Point(BarItemRect.Right, BarItemRect.Bottom)
						barManager1.GetToolTipController().ShowHint(te, linkPoint)
					Else
						barManager1.GetToolTipController().HideHint()
					End If

				End If

			Return False
		End Function

		#End Region
	End Class
End Namespace