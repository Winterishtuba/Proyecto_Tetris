﻿Imports Microsoft.VisualBasic.Devices

Public Enum BlockRotation As Integer
    deg0
    deg90
    deg180
    deg270
End Enum

Public Enum BlockColor As Integer
    None
    Red
    Blue
    Yellow
    Purple
End Enum

Public Enum BlockType As Integer
    IBlock
    OBlock
    LBlock
    JBlock
    TBlock
    SBlock
    ZBlock
    PlusBlock
    BruhBlock
End Enum
Public Class GameGrid
    Public grid(20, 10) As Integer
    Public columns = 10
    Public rows = 20

    Function inBounds(myPoint As Point) As Boolean
        Return (0 <= myPoint.X And myPoint.X <= columns) And (0 <= myPoint.Y And myPoint.Y <= rows)
    End Function

    Function isEmpty(myPoint As Point) As Boolean
        Return grid(myPoint.X, myPoint.Y).Equals(0)
    End Function

    Function isValidPos(myPoint As Point) As Boolean
        Return inBounds(myPoint) And isEmpty(myPoint)
    End Function

    Function isRowEmpty(row As Integer) As Boolean
        'Account for spawning area
        row = row + 2
        For i As Integer = 0 To (columns - 1)
            If Not grid(row, i).Equals(0) Then
                Return False
            End If
        Next
        Return True
    End Function

    Function isRowFull(row As Integer) As Boolean
        row = row + 2
        For i As Integer = 0 To (columns - 1)
            If grid(row, i).Equals(0) Then
                Return False
            End If
        Next
        Return True
    End Function

    Sub emptyRow(row As Integer)
        row = row + 2
        For i As Integer = 0 To (columns - 1)
            grid(row, i) = 0
        Next
        Return
    End Sub

    Sub clearRows()
        Dim clearedRows = 0
        Dim curRow = 0
        While Not isRowEmpty(curRow)
            If isRowFull(curRow) Then
                emptyRow(curRow)
                clearedRows = clearedRows + 1
            Else
                For i As Integer = 0 To (columns - 1)
                    grid(curRow - clearedRows, i) = grid(curRow, i)
                Next
            End If
            curRow = curRow + 1
        End While
    End Sub

End Class

Public Class Block


    Public spawnPos As New Point(0, 4)
    Public spawnOffset As Point
    Public position As Point
    Public rotation As BlockRotation
    Public color As BlockColor
    Public tileCount As Integer
    Public tiles(,) As Point
    Public type As BlockType

    Sub New()
        rotation = 0
        'randomize
        Static Dim gen As System.Random = New System.Random()
        Dim typeArray = {BlockType.IBlock, BlockType.OBlock, BlockType.LBlock, BlockType.JBlock, BlockType.TBlock, BlockType.SBlock, BlockType.ZBlock, BlockType.PlusBlock}
        Dim colorArray = {BlockColor.Red, BlockColor.Blue, BlockColor.Yellow, BlockColor.Purple}
        Me.type = typeArray(gen.Next(0, 8))
        Me.color = colorArray(gen.Next(1, 4))
        Select Case type
            Case BlockType.IBlock
                tiles = {
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(1, 3)},
                            {New Point(0, 2), New Point(1, 2), New Point(2, 2), New Point(3, 2)},
                            {New Point(2, 0), New Point(2, 1), New Point(2, 2), New Point(2, 3)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 1), New Point(3, 1)}
                           }
                tileCount = 4
                spawnOffset = New Point(0, -1)
            Case BlockType.OBlock
                tiles = {
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.JBlock
                tiles = {
                            {New Point(0, 0), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(0, 2), New Point(1, 1), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 0), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.LBlock
                tiles = {
                            {New Point(0, 2), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 1), New Point(2, 2)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 0)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.SBlock
                tiles = {
                            {New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 1), New Point(1, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(1, 1), New Point(1, 2), New Point(2, 0), New Point(2, 1)},
                            {New Point(0, 0), New Point(1, 0), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.TBlock
                tiles = {
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.ZBlock
                tiles = {
                            {New Point(0, 0), New Point(0, 1), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 2), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(2, 1), New Point(2, 2)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(2, 0)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.PlusBlock
                tiles = {
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)}
                        }
                tileCount = 5
                spawnOffset = New Point(0, 0)
            Case BlockType.BruhBlock
                tiles = {
                            {New Point(0, 0), New Point(1, 0), New Point(2, 1), New Point(2, 2)},
                            {New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(2, 0)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(0, 2), New Point(1, 2), New Point(2, 0), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
        End Select
        position.X = spawnPos.X + spawnOffset.X
        position.Y = spawnPos.Y + spawnOffset.Y
    End Sub

    Sub New(ByVal type As BlockType)
        rotation = 0
        'randomize
        Static Dim gen As System.Random = New System.Random()
        Dim typeArray = {BlockType.IBlock, BlockType.OBlock, BlockType.LBlock, BlockType.JBlock, BlockType.TBlock, BlockType.SBlock, BlockType.ZBlock, BlockType.PlusBlock}
        Dim colorArray = {BlockColor.Red, BlockColor.Blue, BlockColor.Yellow, BlockColor.Purple}
        Do
            Me.type = typeArray(gen.Next(0, 8))
        Loop While Me.type = type

        Me.color = colorArray(gen.Next(1, 4))
        Me.type = type
        Select Case type
            Case BlockType.IBlock
                tiles = {
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(1, 3)},
                            {New Point(0, 2), New Point(1, 2), New Point(2, 2), New Point(3, 2)},
                            {New Point(2, 0), New Point(2, 1), New Point(2, 2), New Point(2, 3)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 1), New Point(3, 1)}
                           }
                tileCount = 4
                spawnOffset = New Point(0, -1)
            Case BlockType.OBlock
                tiles = {
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 0), New Point(1, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.JBlock
                tiles = {
                            {New Point(0, 0), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(0, 2), New Point(1, 1), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 0), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.LBlock
                tiles = {
                            {New Point(0, 2), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(2, 1), New Point(2, 2)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 0)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.SBlock
                tiles = {
                            {New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(1, 1)},
                            {New Point(0, 1), New Point(1, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(1, 1), New Point(1, 2), New Point(2, 0), New Point(2, 1)},
                            {New Point(0, 0), New Point(1, 0), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(1, 0)
            Case BlockType.TBlock
                tiles = {
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 1), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.ZBlock
                tiles = {
                            {New Point(0, 0), New Point(0, 1), New Point(1, 1), New Point(1, 2)},
                            {New Point(0, 2), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(1, 0), New Point(1, 1), New Point(2, 1), New Point(2, 2)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(2, 0)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
            Case BlockType.PlusBlock
                tiles = {
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)},
                            {New Point(0, 1), New Point(1, 0), New Point(1, 1), New Point(1, 2), New Point(2, 1)}
                        }
                tileCount = 5
                spawnOffset = New Point(0, 0)
            Case BlockType.BruhBlock
                tiles = {
                            {New Point(0, 0), New Point(1, 0), New Point(2, 1), New Point(2, 2)},
                            {New Point(0, 1), New Point(0, 2), New Point(1, 0), New Point(2, 0)},
                            {New Point(0, 0), New Point(0, 1), New Point(1, 2), New Point(2, 2)},
                            {New Point(0, 2), New Point(1, 2), New Point(2, 0), New Point(2, 1)}
                        }
                tileCount = 4
                spawnOffset = New Point(0, 0)
        End Select
        position.X = spawnPos.X + spawnOffset.X
        position.Y = spawnPos.Y + spawnOffset.Y
    End Sub

    Function getTiles() As List(Of Point)
        Dim list As New List(Of Point)
        For i As Integer = 0 To (tileCount - 1)
            list.Add(New Point(position.X + tiles(rotation, i).X, position.Y + tiles(rotation, i).Y))
        Next
        Return list
    End Function

    Sub rotateClockwise()
        rotation = (rotation + 1) Mod 4
    End Sub

    Sub rotateCounterClockwise()
        If rotation = 0 Then
            rotation = 3
            Return
        End If
        rotation = rotation - 1
    End Sub

    Sub move(ByVal myPoint As Point)
        position.X = position.X + myPoint.X
        position.Y = position.Y + myPoint.Y
    End Sub

End Class


Class BlockQueue
    Public queue As New List(Of Block)
    Public lastType As BlockType
    Sub New()
        queue.Add(New Block())
        lastType = queue(0).type
        queue.Add(New Block(lastType))
        lastType = queue(1).type
        queue.Add(New Block(lastType))
        lastType = queue(2).type
    End Sub
    Function nextBlock() As Block
        Dim temp As Block = queue(0)
        Dim block As New Block(lastType)
        lastType = block.type
        queue.RemoveAt(0)
        queue.Add(block)
        Return temp
    End Function

End Class

Class GameState
    Public currentBlock As Block
    Public ghostBlock As Block
    Public grid As GameGrid
    Public queue As BlockQueue
    Public gameOver As Boolean = False
    Public heldBlock As Block

    Sub New()
        queue = New BlockQueue()
        currentBlock = queue.nextBlock()
        grid = New GameGrid()
        updateGhost()
    End Sub

    Function blockFits(block As Block) As Boolean
        Dim listOfTiles = block.getTiles()
        For Each point As Point In listOfTiles
            If Not grid.isValidPos(point) Then
                Return False
            End If
        Next
        Return True
    End Function

    Sub rotateClockwise()
        currentBlock.rotateClockwise()
        If Not blockFits(currentBlock) Then
            currentBlock.rotateCounterClockwise()
        End If
        updateGhost()
    End Sub

    Sub rotateCounterClockwise()
        currentBlock.rotateCounterClockwise()
        If Not blockFits(currentBlock) Then
            currentBlock.rotateClockwise()
        End If
        updateGhost()
    End Sub

    Sub moveLeft()
        currentBlock.move(New Point(0, -1))
        If Not blockFits(currentBlock) Then
            currentBlock.move(New Point(0, 1))
        End If
        updateGhost()
    End Sub

    Sub moveRight()
        currentBlock.move(New Point(0, 1))
        If Not blockFits(currentBlock) Then
            currentBlock.move(New Point(0, -1))
        End If
        updateGhost()
    End Sub

    Function isGameOver()
        Return (Not grid.isRowEmpty(0)) And (Not grid.isRowEmpty(1))
    End Function

    Sub placeBlock()
        Dim listOfPos = currentBlock.getTiles()
        For Each point As Point In listOfPos
            grid.grid(point.X, point.Y) = currentBlock.color
        Next
        grid.clearRows()
        If isGameOver() Then
            gameOver = True
        Else
            currentBlock = queue.nextBlock()
            updateGhost()
        End If
    End Sub

    Sub moveDown()
        currentBlock.move(New Point(1, 0))
        If Not blockFits(currentBlock) Then
            currentBlock.move(New Point(-1, 0))
            placeBlock()
        End If
    End Sub

    Sub jumpDown()
        For i As Integer = 0 To (grid.rows - 1)
            currentBlock.move(New Point(1, 0))
            If Not blockFits(currentBlock) Then
                currentBlock.move(New Point(-1, 0))
                placeBlock()
                Exit For
            End If
        Next
    End Sub


    Sub updateGhost()
        ghostBlock = currentBlock
        For i As Integer = 0 To (grid.rows - 1)
            ghostBlock.move(New Point(1, 0))
            If Not blockFits(ghostBlock) Then
                ghostBlock.move(New Point(-1, 0))
                Exit For
            End If
        Next
    End Sub



End Class