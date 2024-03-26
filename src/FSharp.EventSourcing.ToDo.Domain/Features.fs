module FSharp.EventSourcing.ToDo.Domain.Features

open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Task

let buildState (events: TaskEvent list) =
    events |> List.fold decider.evolve decider.initialState

let applyEvents task (events: TaskEvent list) = events |> List.fold decider.evolve task

let applyEvent task (event: TaskEvent) = decider.evolve task event

let createTask (cmd: CreateTaskCommand) =
    TaskCommand.CreateTask cmd |> decider.decide decider.initialState

let assignTask (cmd: AssignTaskCommand) task =
    TaskCommand.AssignTask cmd |> decider.decide task

let addComment (cmd: AddCommentCommand) task =
    TaskCommand.AddComment cmd |> decider.decide task

let completeTask (cmd: CompleteTaskCommand) task =
    TaskCommand.CompleteTask cmd |> decider.decide task
