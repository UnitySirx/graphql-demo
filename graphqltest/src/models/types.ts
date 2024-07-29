// 定义 Author 类型
export interface Author {
    name: string;
}

// 定义 Book 类型
export interface Book {
    title: string;
    author: Author;
}

// 定义 GraphQL 查询返回的类型
export interface GetBookQuery {
    book: Book;
}
