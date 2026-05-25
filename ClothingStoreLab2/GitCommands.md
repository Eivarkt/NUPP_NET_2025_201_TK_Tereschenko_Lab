# Git commands for Lab 2

```bash
git checkout master
git pull
git checkout -b lab2

# after changes
git add .
git commit -m "Add lab 2 async CRUD service"
git push -u origin lab2
```

Після цього потрібно створити Pull Request з `lab2` у `master`. Якщо PR з `lab1` ще не закритий, тоді PR потрібно робити з `lab2` у `lab1`.
