module FSharp.EventSourcing.ToDo.Domain.Functions

open System
open FSharp.EventSourcing.ToDo.Domain.Types

let handleCreateTaskCommand (cmd: CreateTaskCommand) (task: TaskState) =
    match task with
    | Empty ->
        Ok
        <| TaskEvent.TaskCreated
            { TaskId = cmd.Id
              Title = cmd.Title
              Description = cmd.Description
              AuthorId = cmd.AuthorId
              CreatedAt = cmd.Date
              Priority = cmd.Priority }
    | _ -> Error "Task already exists"


let handleAssignTaskCommand (cmd: AssignTaskCommand) (task: TaskState) =
    match task with
    | Completed _ -> Error "Cannot assign a completed task"
    | _ ->
        Ok
        <| TaskEvent.TaskAssigned
            { AssignedTo = cmd.AssignedTo
              AssignedAt = cmd.Date }

let handleAddCommentCommand (cmd: AddCommentCommand) (task: TaskState) =
    match task with
    | Completed _ -> Error "Cannot comment on a completed task"
    | _ -> Ok <| TaskEvent.TaskCommented { Comment = cmd.Comment }

let handleCompleteTaskCommand (cmd: CompleteTaskCommand) (task: TaskState) =
    match task with
    | Completed _ -> Error "Task is already completed"
    | Empty -> Error "Unable to complete a task that does not exist"
    | Open _ -> Ok <| TaskEvent.TaskCompleted { CompletedAt = cmd.Date }


let createTask (event: TaskCreatedEvent) =
    TaskState.Open
        { Id = event.TaskId
          Title = event.Title
          Description = event.Description
          AuthorId = event.AuthorId
          CreatedAt = event.CreatedAt
          Comments = List.Empty
          Priority = event.Priority
          Assigment = None }

let completeTask (task: OpenTask) (event: TaskCompletedEvent) =
    TaskState.Completed
        { Task = task
          CompletedAt = event.CompletedAt }


let applyEvent (task: OpenTask) (event: TaskEvent) =
    match event with
    | TaskEvent.TaskAssigned e ->
        { task with
            Assigment =
                Some
                    { AssignedTo = e.AssignedTo
                      AssignedAt = e.AssignedAt } }
    | TaskEvent.TaskCommented e ->
        { task with
            Comments = task.Comments @ [ e.Comment ] }
    | TaskEvent.TaskCreated _ -> task
    | TaskEvent.TaskCompleted _ -> task
