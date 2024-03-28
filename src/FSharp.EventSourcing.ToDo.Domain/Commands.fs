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
    AssignedTo: UserId
    Date: DateTimeOffset
}

type AddCommentArgs = {
    Comment: Comment
    AuthorId: UserId
    Date: DateTimeOffset
}

type CompleteTaskArgs = {
    Date: DateTimeOffset
}

type TaskCommand =
    | CreateTask of CreateTaskArgs
    | AssignTask of AssignTaskArgs
    | AddComment of AddCommentArgs
    | CompleteTask of CompleteTaskArgs


