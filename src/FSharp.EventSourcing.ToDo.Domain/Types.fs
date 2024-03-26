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
type CreateTaskCommand = {
    Id: TaskId
    Title: string
    Description: string
    Priority: TaskPriority
    Date: DateTimeOffset
    AuthorId: UserId
}

type AssignTaskCommand = {
    TaskId: TaskId
    AssignedTo: UserId
    Date: DateTimeOffset
}

type AddCommentCommand = {
    TaskId: TaskId
    Comment: Comment
    AuthorId: UserId
    Date: DateTimeOffset
}

type CompleteTaskCommand = {
    TaskId: TaskId
    Date: DateTimeOffset
}

type TaskCommand =
    | CreateTask of CreateTaskCommand
    | AssignTask of AssignTaskCommand
    | AddComment of AddCommentCommand
    | CompleteTask of CompleteTaskCommand



//////////////////////////////////////////
/// Events
//////////////////////////////////////////
type TaskCreatedEvent = {
    TaskId: TaskId
    Title: string
    Description: string
    AuthorId: UserId
    CreatedAt: DateTimeOffset
    Priority: TaskPriority
}

type TaskAssignedEvent = {
    AssignedTo: UserId
    AssignedAt: DateTimeOffset
}

type TaskCommentedEvent = {
    Comment: Comment
}

type TaskCompletedEvent = {
    CompletedAt: DateTimeOffset
}

type TaskEvent =
    | TaskCreated of TaskCreatedEvent
    | TaskAssigned of TaskAssignedEvent
    | TaskCommented of TaskCommentedEvent
    | TaskCompleted of TaskCompletedEvent
    



