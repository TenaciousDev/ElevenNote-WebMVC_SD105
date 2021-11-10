## POSTMAN - ADVANCED FEATURES

Resource:
[Postman Learning Center](https://learning.postman.com/docs/getting-started/introduction/)

1. Intro

- Go over basic Postman features (refresher)
- Highlight using folders to help organize
- Show saving response as example

  > DEMONSTRATE Get Category By Id error handling response

---

2. Variables & Scope

Resource: [Dynamic variables (Postman Learning Center)](https://learning.postman.com/docs/writing-scripts/script-references/variables-list/)

Two Kinds Of Variables:

- built-in variables using [faker library](https://www.npmjs.com/package/faker)
  - show `$randomLoremWords` variable
  - show `$randomLoremParagraphs` variable
- user-defined variables

  > - DEMONSTRATE `{{email}}` and `{{password}}` variables

Different scopes:

- global variables
- environmental variables
- collection-scoped variables

---

3. Environments, Workspaces, & Collections

An **environment** is a set of variables that allow you to switch the context of your requests

- environmental variables
- contained within a workspace

**Workspaces** allow you to organize your Postman work and collaborate with teammates

- global variables

- Resource: [Workspaces (Postman Learning Center)](https://learning.postman.com/docs/collaborating-in-postman/using-workspaces/creating-workspaces/)

- Visibility & Collaboration

  - Personal workspace is visible only to you
  - Public workspace is visible to anyone across the internet
  - Team workspace is like a shared Personal workspace, you can share with collaborators and manage access

**Collections** are a group of saved requests you can organize into folders.

- Folder Organization
- Saving Request Output as Example
- Authorization & Inheriting from Parent
  - inheritance across collection
  - inheritance per folder

> DEMONSTRATE `{{userOneToken}}` variable to hold the `Bearer token`
>
> - use the `Set as variable` functionality

---

4. Testing in Postman

You can add JavaScript to execute after sending an HTTP request in Postman.

You can add these tests to a collection, folder, or single request.

- discuss the purpose of testing in Postman

  - verify your data
  - verify response meets intent

- point out that Postman offers some quick scaffolding of common tests

> DEMONSTRATE using the `Status Code: Code is 200` built-in test at the folder level (`Notes` folder)

> DEMONSTRATE using the `Response time is less than 200ms` built-in test at the collection level

> DEMONSTRATE:
>
> - In ElevenNote Collection:
>   - Paste/type following script into `Get Categories` request test area.

```js
pm.test("More Than Four Categories", function () {
  var numberOfCategories = pm.response.json().length;
  pm.expect(numberOfCategories).to.greaterThan(4);
});
```

5. Setting Variables w/ Pre-Request Scripts

> DEMONSTRATE:
>
> - Go to `Create Note` request
>   - discuss how it would be convenient to randomly assign a category to the note, so all data is randomly generated to cut down on testing time
>   - demonstrate the functionality of placing the script below into the `Pre-req` section
>     - be sure to use the `{{randomNumber}}` variable as the value for `CategoryId` key

```js
pm.collectionVariables.set("randomNumber", _.random(1, 5));
```

---

6. Generating Documentation (brief)

Resource: [Documenting your API (Postman Learning Center)](https://learning.postman.com/docs/publishing-your-api/documenting-your-api/)

- provide link to docs about generating documentation
- show MadHampsters docs as example of how to use this feature

---

7. Collaboration (share link)

Resource: [Collaborating In Postman (Postman Learning Center)](https://learning.postman.com/docs/collaborating-in-postman/collaboration-intro/)

- Postman allows all users to collaborate with their teams through Team Workspaces.
  - provide link to docs collaboration section

---

---

## ROUTING - ADDITIONAL TOOLS

Resource:
[Attribute Routing in Web API 2 (Microsoft)](https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2)

1. `[Route]`

- The `[Route]` annotation is used to specify the remainder of the URL, after the prefix. It can be specified as a string, and has built-in interpolation to allow method parameters to become part of the endpoint's URL.

> DEMONSTRATE specifying custom URLs by using the `[Route]` annotation to specify the path as a string, and also interpolating method parameters.

```csharp
        [Route("api/Note/GetBy/{id}")]   // <----- add this
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(CreateNoteService().GetNoteById(id));
        }
```

2. `[RoutePrefix]`

- The `[RoutePrefix]` annotation can be applied to the entire controller, and customizes the part of the URL after the base URL (`https://localhost:XXXX`) - that is, the controller part of the URL (Note, Category, etc.)

- `[RoutePrefix]` must be used in conjunction with the `[Route]` annotation on the Controller methods.

> DEMONSTRATE using the `[RoutePrefix]` annotation to change the controller URL from 'Note' to 'Notes'; DEMONSTRATE in Postman how this alone does not change the native pathing of the endpoint.

```csharp
        [Route]    // <----- add this
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(CreateNoteService().GetNotes());
        }
```

> DEMONSTRATE altering `GetBy/{id}` to work with `[RoutePrefix("api/Notes")]`

> DEMONSTRATE a method to get by title of note (have service method pre-built)

```csharp
[Route("GetBy/{title}")]
[HttpGet]
public IHttpActionResult Get(string title)
{
    return Ok(CreateNoteService().GetNoteByTitle(title));
}
```

> DEMONSTRATE what happens if we path both requests as `GetBy/{param}`, no matter the type of param

> DEMONSTRATE changing to `GetById/{id}` and `GetByTitle/{title}`, and the successful request/response in Postman
>
> _Note: URLs are encoded as RFC 1738, which specifies %20 == the space character_
