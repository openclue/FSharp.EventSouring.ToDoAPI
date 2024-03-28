module FSharp.EventSourcing.ToDo.Domain.Events

open System
open FSharp.EventSourcing.ToDo.Domain.Types

type TaskCreatedArgs =
    { TaskId: TaskId
      Title: string
      Description: string
      AuthorId: UserId
      CreatedAt: DateTimeOffset
      Priority: TaskPriority }

type TaskAssignedArgs =
    { AssignedTo: UserId
      AssignedAt: DateTimeOffset }

type TaskCommentedArgs = { Comment: Comment }

type TaskCompletedArgs = { CompletedAt: DateTimeOffset }

type TaskEvent =
    | TaskCreated of TaskCreatedArgs
    | TaskAssigned of TaskAssignedArgs
    | TaskCommented of TaskCommentedArgs
    | TaskCompleted of TaskCompletedArgs
