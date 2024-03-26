module FSharp.EventSourcing.ToDo.Domain.Tests.CreateTaskTests

open System
open FsUnit.Xunit
open FSharp.EventSourcing.ToDo.Domain
open FSharp.EventSourcing.ToDo.Domain.Types
open FSharp.EventSourcing.ToDo.Domain.Features
open Xunit

let initialState = Task.decider.initialState

let createTaskCommand: CreateTaskCommand =
    { Id = TaskId <| Guid.NewGuid()
      Title = "Test task"
      Description = "This is a test task"
      Priority = TaskPriority.Medium
      AuthorId = UserId <| Guid.NewGuid()
      Date = DateTimeOffset.Now }

let taskCreatedEvent: TaskEvent =
    TaskCreated
        { Title = createTaskCommand.Title
          Description = createTaskCommand.Description
          Priority = createTaskCommand.Priority
          AuthorId = createTaskCommand.AuthorId
          TaskId = createTaskCommand.Id
          CreatedAt = createTaskCommand.Date }

let expectedState: TaskState =
    Open
        { Id = createTaskCommand.Id
          Assigment = None
          Comments = List.Empty
          Title = createTaskCommand.Title
          Description = createTaskCommand.Description
          Priority = createTaskCommand.Priority
          AuthorId = createTaskCommand.AuthorId
          CreatedAt = createTaskCommand.Date }


[<Fact>]
let ``Given CreateTaskCommand When createTask Then valid event emitted`` () =

    let event = createTask createTaskCommand

    match event with
    | Ok e -> e |> should equal taskCreatedEvent
    | Error e -> failwith e


[<Fact>]
let ``Given TaskCreatedEvent When apply event Then valid state created`` () =

    let state = taskCreatedEvent |> applyEvent initialState

    state |> should equal expectedState
