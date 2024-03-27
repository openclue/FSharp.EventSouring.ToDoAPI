module FSharp.EventSourcing.ToDo.Domain.Features

open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Task

let applyEvent task (event: TaskEvent) = decider.evolve task event

let applyEvents task (events: TaskEvent list) = events |> List.fold applyEvent task

let buildState (events: TaskEvent list) =
    events |> applyEvents decider.initialState

let executeCommand task (cmd: TaskCommand) = cmd |> decider.decide task

let createTask (cmd: CreateTaskCommand) =
    TaskCommand.CreateTask cmd |> executeCommand decider.initialState

let assignTask (cmd: AssignTaskCommand) task =
    TaskCommand.AssignTask cmd |> executeCommand task

let addComment (cmd: AddCommentCommand) task =
    TaskCommand.AddComment cmd |> executeCommand task

let completeTask (cmd: CompleteTaskCommand) task =
    TaskCommand.CompleteTask cmd |> executeCommand task
