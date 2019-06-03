# Hello World Web Application


[![Build Status](https://dev.azure.com/antonbrigantimyob/HelloWorldFMA/_apis/build/status/antonbriganti-myob.HelloWorldWebApp?branchName=master)](https://dev.azure.com/antonbrigantimyob/HelloWorldFMA/_build/latest?definitionId=1&branchName=master)

## Using the application
### API interactions
- **GET /api/time** - Get time message with people 
- **GET /api/people** - Get list of people
- **POST /api/people** - Add person to world. 201 on pass, 400 on fail. Can't add people with the same name.
- **DELETE /api/people** - Remove person from world. 200 on pass, 400 or 404 on fail. Person must exist to be deleted, can not remove owner (Anton)
- **PUT /api/people** - Update person's name in world. 200 on pass, 400 on fail. Person must exist to be updated, new name can't exist already, can't change owner's name

### Running tests
From the project root directory (or the test project directory `HelloWorldWebAppTests`)
```
dotnet test
```
