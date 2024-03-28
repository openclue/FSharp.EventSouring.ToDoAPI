module FSharp.EventSourcing.ToDo.Domain.Commands

open System
open FSharp.EventSourcing.ToDo.Domain.Types

type CreateTaskArgs = {
    Id: TaskId
    Title: string
    Description: string
    Priority: TaskPriority
    Date: DateTimeOffset
    AuthorId: UserId
}

type AssignTaskArgs = {
    TaskId: TaskId
    AssignedTo: UserId
    Date: DateTimeOffset
}

type AddCommentArgs = {
    TaskId: TaskId
    Comment: Comment
    AuthorId: UserId
    Date: DateTimeOffset
}

type CompleteTaskArgs = {
    TaskId: TaskId
    Date: DateTimeOffset
}

type TaskCommand =
    | CreateTask of CreateTaskArgs
    | AssignTask of AssignTaskArgs
    | AddComment of AddCommentArgs
    | CompleteTask of CompleteTaskArgs


