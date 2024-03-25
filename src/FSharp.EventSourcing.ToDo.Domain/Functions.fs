module FSharp.EventSourcing.ToDo.Domain.Functions

open System
open FSharp.EventSourcing.ToDo.Domain.Types

let createTask (cmd: CreateTaskCommand) (task: Task) =
    match task with
    | None ->
        Ok
        <| TaskEvent.TaskCreated
            { TaskId = cmd.Id
              Title = cmd.Title
              Description = cmd.Description
              AuthorId = cmd.AuthorId
              CreatedAt = cmd.Date }
    | _ -> Error "Task already exists"


let assignTask (cmd: AssignTaskCommand) (task: Task) =
    match task with
    | CompletedTask _ -> Error "Cannot assign a completed task"
    | _ ->
        Ok
        <| TaskEvent.TaskAssigned
            { TaskId = cmd.TaskId
              AssignedTo = cmd.AssignedTo
              AssignedAt = cmd.Date }

let commentTask (cmd: AddCommentCommand) (task: Task) =
    match task with
    | CompletedTask _ -> Error "Cannot comment on a completed task"
    | _ ->
        Ok
        <| TaskEvent.TaskCommented
            { TaskId = cmd.TaskId
              Comment = cmd.Comment
              AuthorId = cmd.AuthorId
              CreatedAt = cmd.Date }

let completeTask (cmd: CompleteTaskCommand) (task: Task) =
    match task with
    | CompletedTask _ -> Error "Task is already completed"
    | NewTask _ -> Error "Task is not assigned"
    | _ ->
        Ok
        <| TaskEvent.TaskCompleted
            { TaskId = cmd.TaskId
              CompletedAt = cmd.Date }
