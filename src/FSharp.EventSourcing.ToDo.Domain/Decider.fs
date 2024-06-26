namespace FSharp.EventSourcing.ToDo.Domain

open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Commands
open FSharp.EventSourcing.ToDo.Domain.Events
open FSharp.EventSourcing.ToDo.Domain.Functions

module Decider =

    type Decider<'C, 'E, 'S, 'ERR> =
        { decide: 'S -> 'C -> Result<'E, 'ERR>
          evolve: 'S -> 'E -> 'S
          initialState: 'S
          isComplete: 'S -> bool }

    let decide (state: TaskState) (command: TaskCommand) =
        match command with
        | CreateTask c -> handleCreateTaskCommand c state
        | AssignTask c -> handleAssignTaskCommand c state
        | AddComment c -> handleAddCommentCommand c state
        | CompleteTask c -> handleCompleteTaskCommand c state

    let evolve (state: TaskState) (event: TaskEvent) =
        match state, event with
        | Empty, TaskCreated e -> createTask e
        | Open task, TaskCompleted e -> completeTask task e
        | Open task, _ -> TaskState.Open <| applyEvent task event
        | _ -> state

    let isComplete (state: TaskState) =
        match state with
        | Completed _ -> true
        | _ -> false

module Task =
    open Decider

    let decider =
        { decide = decide
          evolve = evolve
          initialState = Empty
          isComplete = isComplete }
