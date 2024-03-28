module FSharp.EventSourcing.ToDo.Domain.Types

open System

type TaskId = TaskId of Guid

type UserId = UserId of Guid

type Comment = {
    AuthorId: UserId
    Content: string
    CreatedAt: DateTimeOffset
}

type TaskPriority =
    | Low
    | Medium
    | High
    
type TaskAssigment = {
    AssignedTo: UserId
    AssignedAt: DateTimeOffset
}

type OpenTask = {
    Id: TaskId
    Title: string
    Description: string
    AuthorId: UserId
    Assigment: TaskAssigment option
    CreatedAt: DateTimeOffset
    Priority: TaskPriority
    Comments: Comment list
}

type CompletedTask = {
    Task: OpenTask
    CompletedAt: DateTimeOffset
}

type TaskState =
    | Empty
    | Open of OpenTask
    | Completed of CompletedTask
    

//////////////////////////////////////////
// Commands
//////////////////////////////////////////

//////////////////////////////////////////
/// Events
//////////////////////////////////////////
    



