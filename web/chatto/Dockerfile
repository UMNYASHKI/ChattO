FROM node:18.18-alpine

RUN npm install -g pnpm

RUN mkdir -p /app

WORKDIR /app

COPY package.json pnpm-lock.yaml ./

RUN pnpm install

COPY . .

RUN pnpm build

EXPOSE 3000

CMD pnpm run start
