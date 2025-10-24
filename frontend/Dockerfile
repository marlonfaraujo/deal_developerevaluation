FROM node:22

WORKDIR /workspace/frontend

COPY frontend/package*.json ./
#RUN npm install -g @angular/cli@19
RUN npm ci
COPY frontend/ ./

RUN npm run build

EXPOSE 4200

ENV DEVCONTAINER=true
ENV CHOKIDAR_USEPOLLING=true

USER node

#ENTRYPOINT ["tail", "-f", "/dev/null"]

ENTRYPOINT ["npm", "run", "docker"]


