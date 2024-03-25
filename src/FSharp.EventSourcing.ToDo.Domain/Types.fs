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
    
type NewTask = {
   Id: TaskId
   Title: string
   Description: string
   AuthorId: UserId
   CreatedAt: DateTimeOffset
}

type AssignedTask = {
    Id: TaskId
    Title: string
    Description: string
    AuthorId: UserId
    AssignedTo: UserId
    CreatedAt: DateTimeOffset
    AssignedAt: DateTimeOffset
    Priority: TaskPriority
    Comments: Comment list
}

type CompletedTask = {
    Id: TaskId
    Title: string
    Description: string
    AuthorId: UserId
    AssignedTo: UserId
    CreatedAt: DateTimeOffset
    AssignedAt: DateTimeOffset
    CompletedAt: DateTimeOffset
    Priority: TaskPriority
    Comments: Comment list
}

type Task =
    | None
    | NewTask of NewTask
    | AssignedTask of AssignedTask
    | CompletedTask of CompletedTask
    

//////////////////////////////////////////
// Commands
//////////////////////////////////////////
type CreateTaskCommand = {
    Id: TaskId
    Title: string
    Description: string
    Priority: TaskPriority
}

type AssignTaskCommand = {
    TaskId: TaskId
    AssignedTo: UserId
}

type AddCommentCommand = {
    TaskId: TaskId
    Comment: Comment
}

type CompleteTaskCommand = {
    TaskId: TaskId
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
}

type TaskAssignedEvent = {
    TaskId: TaskId
    AssignedTo: UserId
    AssignedAt: DateTimeOffset
}

type TaskCommentedEvent = {
    TaskId: TaskId
    AuthorId: UserId
    Comment: Comment
    CreatedAt: DateTimeOffset
}

type TaskCompletedEvent = {
    TaskId: TaskId
    CompletedAt: DateTimeOffset
}

type TaskEvent =
    | TaskCreated of TaskCreatedEvent
    | TaskAssigned of TaskAssignedEvent
    | TaskCommented of TaskCommentedEvent
    | TaskCompleted of TaskCompletedEvent
    



