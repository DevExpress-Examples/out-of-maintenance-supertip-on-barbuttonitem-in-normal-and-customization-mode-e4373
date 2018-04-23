Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraBars

Namespace WindowsApplication103
	Friend Class ToolTipObject
		Public ToolTip As String
		Public ToolTipItem As BarButtonItem
		Public ToolTipCustomizationMode As String
		Public Sub New(ByVal _toolTipItem As BarButtonItem, ByVal _toolTip As String, ByVal _toolTipCustocustomizationMode As String)
			ToolTipItem = _toolTipItem
			ToolTip = _toolTip
			ToolTipCustomizationMode = _toolTipCustocustomizationMode
		End Sub

	End Class
End Namespace
